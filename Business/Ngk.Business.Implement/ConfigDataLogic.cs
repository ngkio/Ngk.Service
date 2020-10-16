using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;
using Ngk.DataAccess.Interface.Mongo;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Ngk.Business.Implement
{
    public class ConfigDataLogic : BaseBusinessLogic<ConfigData, IConfigDataRepository>, IConfigDataLogic
    {
        #region ctor

        private readonly IOperateLogRepository _operateLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ConfigDataLogic(IConfigDataRepository repository, IOperateLogRepository operateLogRepository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _operateLogRepository = operateLogRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        /// <summary>
        /// 根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetByKey(string key)
        {
            return Repository.GetByKey(key);
        }

        /// <summary>
        /// 根据key和语言获取相关的key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetByKeyAndLang(string key)
        {
            String langKey = String.Empty;
            var langType = LanguageHelper.GetEnumLanguageType(_httpContextAccessor.HttpContext);
            if (langType != EnumLanguageType.Zh)
            {
                key = $"{key}_{langType.ToString().ToLower()}";
            }
            var langValue = Repository.GetByKey(langKey);
            if (String.IsNullOrEmpty(langValue))
            {
                if (key != langKey)
                {
                    return Repository.GetByKey(key);
                }
            }
            return langValue;
        }




        /// <summary>
        /// 根据key集合获取配置数据集合
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public List<ConfigData> GetByKeyList(List<string> keyList)
        {
            return Repository.GetByKeyList(keyList);
        }

        /// <summary>
        /// 修改时记录值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="result"></param>
        public override void Update(ConfigData entity, out ExcutedResult result)
        {
            try
            {
                //查询原始数据
                var oldEntity = Repository.GetEntityByKey(entity.ConfigKey);
                var originalValue = JsonConvert.SerializeObject(oldEntity);
                //如果原始数据不存在，返回提示
                if (oldEntity == null)
                {
                    result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "操作失败,数据不存在");
                    return;
                }

                oldEntity.ConfigValue = entity.ConfigValue;
                oldEntity.Remark = entity.Remark;

                using (var trans = base.GetNewTransaction())
                {
                    base.Update(oldEntity, out result);
                    if (result.Status != EnumStatus.Success)
                    {
                        throw new Exception("更新配置实体失败");
                    }

                    if (result.Code == SysResultCode.Success)
                    {
                        OperateLog log = new OperateLog();
                        log.Id = Guid.NewGuid();
                        log.ClientIp = CurrentUser.ClientIP;
                        log.CreateTime = DateTime.UtcNow;
                        log.ManagerAccount = CurrentUser.Mobile;
                        log.ManagerId = CurrentUser.Id;
                        log.OriginalValue = originalValue;
                        log.NewValue = JsonConvert.SerializeObject(entity);
                        log.Operate = "Update";
                        log.Function = "更新配置";
                        _operateLogRepository.Insert(log);
                    }

                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.OperationFailed, $"{BusinessResultCode.OperationFailed}:{e.Message}");
                Log4NetHelper.WriteError(GetType(), e, $"key:{entity.ConfigKey},new value:{entity.ConfigValue}");
                return;
            }
        }

        /// <summary>
        /// 查询-后端
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<ConfigData> GetByParam(string key)
        {
            var data = GetAll();
            data = data.OrderBy(p => p.ConfigKey).ToList();
            if (!string.IsNullOrWhiteSpace(key))
            {
                data = data.Where(p => p.ConfigKey.Contains(key)).ToList();
            }
            return data;
        }


        public PagedResults<ConfigData> GetCconfigListQuery(ConfigDataParam param)
        {
            return Repository.AdvQuery(param);
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        public void RefreshMemoryCache()
        {
            Repository.RefreshCache();
        }

    }
}


