using System;
using System.Linq;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Interface;
using Ngk.DataAccess.Interface.Mongo;
using Ngk.Service.ManagerWebApi.Attribute;
using Ngk.Service.ManagerWebApi.Models;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    [Language]
    public class LogsController : Controller
    {
        private readonly ILoginLogLogic _loginLogLogic;
        private readonly IMonitorLogRepository _monitorLogRepository;
        private readonly IOperateLogRepository _operateLogRepository;
        private readonly ITransferRecordRepository _transferRecordRepository;
        private readonly IConfigDataLogic _configDataLogic;


        public LogsController(IMonitorLogRepository monitorLogRepository, ILoginLogLogic loginLogLogic,
            IOperateLogRepository operateLogRepository, ITransferRecordRepository transferRecordRepository, IConfigDataLogic configDataLogic)
        {
            this._monitorLogRepository = monitorLogRepository;
            _loginLogLogic = loginLogLogic;
            _operateLogRepository = operateLogRepository;
            _transferRecordRepository = transferRecordRepository;
            _configDataLogic = configDataLogic;
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult GetMonitorLogsDataInfo([FromBody]SeachMonitorLogsModel model)
        {
            try
            {
                MonitorLogsParam param = new MonitorLogsParam();
                if (!String.IsNullOrEmpty(model.UserMobile)) param.UserMobile = model.UserMobile;
                if (!String.IsNullOrEmpty(model.ExecuteStartTime)) param.ExecuteStartTime = DateTime.Parse(model.ExecuteStartTime);
                else param.ExecuteStartTime = DateTime.MinValue;
                if (!String.IsNullOrEmpty(model.ExecuteEndTime)) param.ExecuteEndTime = DateTime.Parse(model.ExecuteEndTime);
                else param.ExecuteEndTime = DateTime.MaxValue;
                if (!String.IsNullOrEmpty(model.IP)) param.IP = model.IP;
                if (!String.IsNullOrEmpty(model.Path)) param.Path = model.Path;
                if (!String.IsNullOrEmpty(model.RequestParams)) param.RequestParams = model.RequestParams;
                if (!String.IsNullOrEmpty(model.ResponseParams)) param.ResponseParams = model.ResponseParams;
                if (!String.IsNullOrEmpty(model.ResponseCode)) param.ResponseCode = model.ResponseCode;

                param.PageIndex = model.PageIndex;
                param.PageSize = model.PageSize;
                if (!String.IsNullOrEmpty(model.SortName)) param.SortName = model.SortName;
                param.IsSortOrderDesc = true;
                var result = _monitorLogRepository.GetQuery(param);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 分页获取登录日志数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult GetLoginLogDataInfo([FromBody]LoginLogParam model)
        {
            return _loginLogLogic.QueryLoginLog(model);
        }


        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult QueryOperateLog([FromBody]OperateLogParams model)
        {
            try
            {
                if (model == null) return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                if (String.IsNullOrEmpty(model.SortName)) model.SortName = "CreateTime";
                var dataInfo = _operateLogRepository.QueryOperateLogLogs(model);
                if (dataInfo != null)
                {
                    return ExcutedResult.SuccessResult(dataInfo);
                }
                return ExcutedResult.FailedResult(BusinessResultCode.DataNotExist, " 数据不存在，请刷新!");
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }


        /// <summary>
        /// 转账记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult QueryTransferRecord([FromBody]TransferRecordParam model)
        {
            try
            {
                if (model == null) return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                if (String.IsNullOrEmpty(model.SortName)) model.SortName = "CreateTime";
                var dataInfo = _transferRecordRepository.GetTransferRecord(model);
                if (dataInfo != null)
                {
                    var searchTxDetail = _configDataLogic.GetByKeyAndLang(ConfigDataKey.SearchTxDetail);
                    if (dataInfo.Data != null && dataInfo.Data.Any())
                    {
                        var transfers = dataInfo.Data.ToList();
                        foreach (var transfer in transfers)
                        {
                            if (!String.IsNullOrEmpty(transfer.TransferId))
                            {
                                transfer.TransferId = String.Format("{0}{1}", searchTxDetail, transfer.TransferId);
                            }
                        }

                        dataInfo.Data = transfers;
                    }
                    return ExcutedResult.SuccessResult(dataInfo);
                }
                return ExcutedResult.FailedResult(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}