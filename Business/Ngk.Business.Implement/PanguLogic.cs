using System;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Newtonsoft.Json;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;
using Ngk.DataAccess.Interface.Mongo;
using Pangu.ServiceCenter.DataAccess.Entities;

namespace Ngk.Business.Implement
{
    public class PanguLogic : IPanguLogic
    {
        private readonly IPanguRepository _panguRepository;
        private readonly PrincipalUser _principalUser;
        private readonly IOperateLogRepository _operateLogRepository;

        public PanguLogic(IPanguRepository panguRepository, PrincipalUser principalUser, IOperateLogRepository operateLogRepository)
        {
            _panguRepository = panguRepository;
            _principalUser = principalUser;
            _operateLogRepository = operateLogRepository;
        }

        /// <summary>
        ///  广告——删
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExcutedResult DeleteNoticeInfo(string id)
        {
            var result= _panguRepository.DeleteNoticeInfo(id);
            if (result.Code== SysResultCode.Success)
            {
                OperateLog log = new OperateLog();
                log.Id = Guid.NewGuid();
                log.ClientIp = _principalUser.ClientIP;
                log.CreateTime = DateTime.UtcNow;
                log.ManagerAccount = _principalUser.Mobile;
                log.ManagerId = _principalUser.Id;
                log.OriginalValue = string.Empty;
                log.NewValue = id;
                log.Operate = "Delete";
                log.Function = "公告删除";
                _operateLogRepository.Insert(log);
            }
            return result;
        }

        /// <summary>
        ///  公告——增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult InsertNoticeInfo(NoticeRequestModel model)
        {
            var result = _panguRepository.InsertNoticeInfo(model);
            if (result.Code==SysResultCode.Success)
            {
                OperateLog log = new OperateLog();
                log.Id = Guid.NewGuid();
                log.ClientIp = _principalUser.ClientIP;
                log.CreateTime = DateTime.UtcNow;
                log.ManagerAccount = _principalUser.Mobile;
                log.ManagerId = _principalUser.Id;
                log.OriginalValue = string.Empty;
                log.NewValue = JsonConvert.SerializeObject(model);
                log.Operate = "Insert";
                log.Function = "公告新增";
                _operateLogRepository.Insert(log);
            }
            return result;

        }

        /// <summary>
        ///  公告——查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult QueryNoticeInfo(QueryNoticeRequestModel model)
        {
            return _panguRepository.QueryNoticeInfo(model);
        }

        /// <summary>
        /// 公告——改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult UpdateNoticeInfo(UpdateNoticeRequestModel model)
        {
            Notice oldNotice = null;
            var result = _panguRepository.UpdateNoticeInfo(model,out oldNotice);
            OperateLog log = new OperateLog();
            log.Id = Guid.NewGuid();
            log.ClientIp = _principalUser.ClientIP;
            log.CreateTime = DateTime.UtcNow;
            log.ManagerAccount = _principalUser.Mobile;
            log.ManagerId = _principalUser.Id;
            log.OriginalValue = JsonConvert.SerializeObject(oldNotice);
            log.NewValue = JsonConvert.SerializeObject(model);
            log.Operate = "Update";
            log.Function = "公告修改";
            _operateLogRepository.Insert(log);
            return result;
        }
    }
}
