using System;
using System.Collections.Generic;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Common;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;
using Ngk.DataAccess.Interface.Mongo;
using Pangu.ServiceCenter.DataAccess.Entities;

namespace Ngk.DataAccess.Implement
{
    public class PanguRepository : IPanguRepository
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly PrincipalUser _principalUser;
        public PanguRepository(INoticeRepository noticeRepository, PrincipalUser principalUser)
        {
            _noticeRepository = noticeRepository;
            _principalUser = principalUser;
        }


        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult InsertNoticeInfo(NoticeRequestModel model)
        {
            try
            {
                if (model == null)
                    throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                model.InsertVerify(); ;
                Notice notice = new Notice
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    ServiceName = model.ServiceName,
                    Content = model.Content,
                    IsShutdownSystem = model.IsShutdownSystem,
                    IsOnlyOne = model.IsOnlyOne,
                    CreateTime = model.ExpireTime
                };
                _noticeRepository.Insert(notice);

                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
            catch (Exception ex)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "发生异常，请稍后再试或联系管理员");
            }
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExcutedResult DeleteNoticeInfo(string id)
        {
            try
            {
                Guid guid = Guid.Empty;
                if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out guid))
                    throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                var notice = _noticeRepository.GetById(guid);
                if (notice == null)
                    throw new BusinessException(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
                else
                {
                    _noticeRepository.Delete(notice);
                    return ExcutedResult.SuccessResult();
                }
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
            catch (Exception ex)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "发生异常，请稍后再试或联系管理员");
            }
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="oldNotice"></param>
        /// <returns></returns>
        public ExcutedResult UpdateNoticeInfo(UpdateNoticeRequestModel model, out Notice oldNotice)
        {
            oldNotice = null;
            try
            {
                Guid guid = Guid.Empty;
                if (model == null || !Guid.TryParse(model.Id, out guid))
                    throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                model.UpdateVerify();
                var notice = _noticeRepository.GetById(guid);
                oldNotice = notice;
                if (notice == null)
                    throw new BusinessException(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
                else
                {
                    if (!string.IsNullOrEmpty(model.Content) && model.Content != notice.Content) notice.Content = model.Content;
                    if (!string.IsNullOrEmpty(model.Title) && model.Title != notice.Title) notice.Title = model.Title;
                    if (model.IsShutdownSystem < 2) notice.IsShutdownSystem = model.IsShutdownSystem;
                    if (model.IsOnlyOne < 2 && model.IsOnlyOne != notice.IsOnlyOne) notice.IsOnlyOne = model.IsOnlyOne;
                    if (model.ExpireTime != default(DateTime)) notice.CreateTime = model.ExpireTime;
                    _noticeRepository.Update(notice);
                    return ExcutedResult.SuccessResult();
                }
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
            catch (Exception ex)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "发生异常，请稍后再试或联系管理员");
            }
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult QueryNoticeInfo(QueryNoticeRequestModel model)
        {
            try
            {
                if (model == null)
                    throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                NoticeParams noticeParams = new NoticeParams();
                if (!string.IsNullOrEmpty(model.Title)) noticeParams.Title = model.Title;
                if (!string.IsNullOrEmpty(model.ServiceName)) noticeParams.ServiceName = model.ServiceName;
                if (!string.IsNullOrEmpty(model.Content)) noticeParams.Content = model.Content;
                if (model.IsShutdownSystem != default(byte) && model.IsShutdownSystem < 3) noticeParams.IsShutdownSystem = model.IsShutdownSystem;
                noticeParams.SortName = "CreateTime";
                noticeParams.IsSortOrderDesc = true;
                noticeParams.SortList = new Dictionary<string, bool> { { noticeParams.SortName, noticeParams.IsSortOrderDesc } };

                var dataInfo = _noticeRepository.GetQuery(noticeParams);
                if (dataInfo != null) return ExcutedResult.SuccessResult(dataInfo);
                return ExcutedResult.FailedResult(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
            catch (Exception ex)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "发生异常，请稍后再试或联系管理员");
            }
        }
    }
}
