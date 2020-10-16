using System;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.DTO.Param;
using Ngk.Service.ManagerWebApi.Attribute;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    [Language]
    public class SmsSendController : Controller
    {

        private readonly ISmsSendLogic _smsSendLogic;
        public SmsSendController(ISmsSendLogic smsSendLogic)
        {
            _smsSendLogic = smsSendLogic;
        }

        /// <summary>
        /// 查询短信日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult GetQuery(string mobile, DateTime? startTime, DateTime? endTime, int pageIndex = 1, int pageSize = 10, string sortName = "", bool? order = null)
        {
            try
            {
                SmsSendParam param = new SmsSendParam()
                {
                    Mobile = mobile,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    SortName = "CreateTime",
                    IsSortOrderDesc = true
                };
                if (!string.IsNullOrEmpty(sortName))
                {
                    param.SortName = sortName.Substring(0, 1).ToUpper() + sortName.Substring(1);
                }
                if (order.HasValue)
                {
                    param.IsSortOrderDesc = order.Value;
                }
                if (startTime != null)
                {
                    param.StartTime = startTime.Value.Date;
                }
                if (endTime != null)
                {
                    param.EndTime = endTime.Value.Date.AddDays(1);
                }
                var result = _smsSendLogic.GetQuery(param);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}