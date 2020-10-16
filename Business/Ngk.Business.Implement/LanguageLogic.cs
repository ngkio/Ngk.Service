using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.Business.Implement
{
    public class LanguageLogic : BaseBusinessLogic<Language, ILanguageRepository>, ILanguageLogic
    {
        #region ctor
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOperateLogRepository _operateLogRepository;

        public LanguageLogic(IHttpContextAccessor httpContextAccessor, ILanguageRepository repository, IOperateLogRepository operateLogRepository) : base(repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _operateLogRepository = operateLogRepository;
        }
        #endregion

        #region 获取描述

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="code"></param>
        /// <param name="enumLanguageDataType"></param>
        /// <returns></returns>
        public string GetDescribe(string code, EnumLanguageDataType enumLanguageDataType)
        {
            try
            {
                return Repository.GetDescribe(code, enumLanguageDataType, LanguageHelper.GetEnumLanguageType(_httpContextAccessor.HttpContext));
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, ex.Message);
                return "System Error";
            }
        }

        #endregion

        #region 重写

        public override void Create(Language entity, out ExcutedResult result)
        {
            entity.State = (int)EnumState.Normal;
            entity.CreateTime = DateTime.UtcNow;
            base.Create(entity, out result);
        }

        #endregion

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult InsertLanguage(LanguageDetailRequestModel model)
        {
            try
            {
                if (model == null) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                model.Verify();
                Language data = Repository.GetLanguageByTypeAndCode(model.Code, model.Type);
                if (data != null) throw new BusinessException(BusinessResultCode.DataExist, " 数据已存在");
                model = SubStrInModel(model);
                Language language = new Language()
                {
                    Id = Guid.NewGuid(),
                    Type = model.Type,
                    Code = model.Code,
                    CreateTime = DateTime.UtcNow,
                    Desc = model.Desc,
                    En = string.IsNullOrEmpty(model.En) ? string.Empty : model.En,
                    Ko = string.IsNullOrEmpty(model.Ko) ? string.Empty : model.Ko,
                    Zh = model.Zh
                };
                Create(language, out var result);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode,
                    GetDescribe(businessException.ErrorCode, EnumLanguageDataType.BusinessResultCode));
            }
            catch (Exception exception)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException,
                    GetDescribe(SysResultCode.ServerException, EnumLanguageDataType.BusinessResultCode));
            }
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ExcutedResult DeleteLanguage(string guid)
        {
            try
            {
                Guid id = Guid.Empty;
                if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out id)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                Language language = base.GetById(id);
                if (language == null) throw new BusinessException(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
                language.Deleter = CurrentUser.UserName;
                language.DeleteTime = DateTime.UtcNow;
                language.State = (int)EnumState.Deleted;
                base.Update(language, out var result);
                if (result.Status != EnumStatus.Success)
                {
                    throw new Exception("更新配置实体失败");
                }
                OperateLog log = new OperateLog()
                {
                    Id = Guid.NewGuid(),
                    ClientIp = CurrentUser.ClientIP,
                    CreateTime = DateTime.Now,
                    ManagerAccount = CurrentUser.UserName,
                    ManagerId = CurrentUser.Id,
                    Function = "语言管理",
                    OriginalValue = JsonConvert.SerializeObject(language),
                    Operate = "Delete",
                };
                _operateLogRepository.Insert(log);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode,
                    GetDescribe(businessException.ErrorCode, EnumLanguageDataType.BusinessResultCode));
            }
            catch (Exception exception)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException,
                    GetDescribe(SysResultCode.ServerException, EnumLanguageDataType.BusinessResultCode));
            }

        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult EditLanguage(LanguageDetailRequestModel model)
        {
            try
            {
                if (model == null) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                model.Verify();
                model = SubStrInModel(model);
                Guid id = Guid.Empty;
                if (string.IsNullOrEmpty(model.Guid) || !Guid.TryParse(model.Guid, out id)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                Language language = base.GetById(id);
                if (language == null) throw new BusinessException(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");

                string originalValue = JsonConvert.SerializeObject(language);
                //如果需要更改类型或者类型
                if (model.Type != language.Type && model.Code != language.Code)
                {
                    Language data = Repository.GetLanguageByTypeAndCode(model.Code, model.Type);
                    if (data != null) throw new BusinessException(BusinessResultCode.DataExist, " 数据已存在");
                    language.Code = model.Code;
                    language.Type = model.Type;
                }
                else if (model.Type != language.Type)
                {
                    Language data = Repository.GetLanguageByTypeAndCode(language.Code, model.Type);
                    if (data != null) throw new BusinessException(BusinessResultCode.DataExist, " 数据已存在");
                    language.Type = model.Type;
                }
                else if (model.Code != language.Code)
                {
                    Language data = Repository.GetLanguageByTypeAndCode(model.Code, language.Type);
                    if (data != null) throw new BusinessException(BusinessResultCode.DataExist, " 数据已存在");
                    language.Code = model.Code;
                }
                if (model.Desc != language.Desc) language.Desc = model.Desc;
                language.Zh = model.Zh;
                language.En = model.En;
                language.Ko = model.Ko;

                string newValue = JsonConvert.SerializeObject(language);
                base.Update(language, out var result);
                OperateLog log = new OperateLog()
                {
                    Id = Guid.NewGuid(),
                    ClientIp = CurrentUser.ClientIP,
                    CreateTime = DateTime.Now,
                    ManagerAccount = CurrentUser.UserName,
                    ManagerId = CurrentUser.Id,
                    Function = "语言管理",
                    OriginalValue = originalValue,
                    NewValue = newValue,
                    Operate = "Update",
                };
                _operateLogRepository.Insert(log);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode,
                    GetDescribe(businessException.ErrorCode, EnumLanguageDataType.BusinessResultCode));
            }
            catch (Exception exception)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException,
                    GetDescribe(SysResultCode.ServerException, EnumLanguageDataType.BusinessResultCode));
            }
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult QueryLanguage(LanguageQueryRequestModel model)
        {
            try
            {
                if (model == null) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                if (model.PageIndex == default(int)) model.PageIndex = 1;
                if (model.PageSize == default(int)) model.PageSize = 10;
                DateTime startTime = default(DateTime);
                DateTime.TryParse(model.StartTime, out startTime);
                DateTime endTime = default(DateTime);
                DateTime.TryParse(model.EndTime, out endTime);
                if (string.IsNullOrEmpty(model.SortName))
                {
                    model.SortName = "CreateTime";
                    model.IsSortOrderDesc = true;
                }

                LanguageParam languageParam = new LanguageParam();
                languageParam.Code = model.Code;
                languageParam.Desc = model.Desc;
                languageParam.Type = model.Type;
                languageParam.Content = model.Content;
                languageParam.StartTime = startTime;
                languageParam.EndTime = endTime;
                languageParam.PageIndex = model.PageIndex;
                languageParam.PageSize = model.PageSize;
                languageParam.SortName = model.SortName;
                languageParam.IsSortOrderDesc = model.IsSortOrderDesc;

                var data = Repository.QueryLanguage(languageParam);
                return ExcutedResult.SuccessResult(data);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode,
                    GetDescribe(businessException.ErrorCode, EnumLanguageDataType.BusinessResultCode));
            }
            catch (Exception exception)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException,
                    GetDescribe(SysResultCode.ServerException, EnumLanguageDataType.BusinessResultCode));
            }
        }

        /// <summary>
        /// 字符串超长截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string SubString(string str, int length)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > length)
            {
                str = str.Substring(0, length);
            }
            return str;
        }


        /// <summary>
        /// 对实体中的字符进行截取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private LanguageDetailRequestModel SubStrInModel(LanguageDetailRequestModel model)
        {
            model.Code = SubString(model.Code, PrintPropertyValue<Language>("Code"));
            model.Desc = SubString(model.Desc, PrintPropertyValue<Language>("Desc"));
            model.En = SubString(model.En, PrintPropertyValue<Language>("En"));
            model.Zh = SubString(model.Zh, PrintPropertyValue<Language>("Zh"));
            model.Ko = SubString(model.Ko, PrintPropertyValue<Language>("Ko"));

            return model;
        }


        /// <summary>
        /// 使用反射获取字符串的长度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfoName"></param>
        /// <returns></returns>
        private int PrintPropertyValue<T>(string propertyInfoName)
        {
            int result = 32;
            try
            {
                PropertyInfo[] infos = typeof(T).GetProperties();
                foreach (var propertyInfo in infos)
                {
                    if (string.Equals(propertyInfo.Name, propertyInfoName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        object[] objAttrs = propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute), true);
                        foreach (var objAttr in objAttrs)
                        {
                            if (objAttr is StringLengthAttribute)
                            {
                                StringLengthAttribute stringLengthAttribute = objAttr as StringLengthAttribute;
                                result = stringLengthAttribute.MaximumLength;
                            }
                            break;
                        }
                        break;
                    }
                }
            }
            catch { }
            return result;
        }
    }
}


