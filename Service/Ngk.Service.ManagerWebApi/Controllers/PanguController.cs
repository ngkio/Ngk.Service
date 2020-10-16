using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities;
using Ngk.Service.ManagerWebApi.Attribute;
using Pangu.ServiceCenter.DataAccess.Entities;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    [Language]
    public class PanguController : Controller
    {
        private readonly IPanguLogic _panguLogic;

        public PanguController(IPanguLogic panguLogic)
        {
            _panguLogic = panguLogic;
        }

        /// <summary>
        ///  增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult InsertNoticeInfo([FromBody]NoticeRequestModel model)
        {
            try
            {
                return _panguLogic.InsertNoticeInfo(model);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult UpdateNoticeInfo([FromBody]UpdateNoticeRequestModel model)
        {
            try
            {
                return _panguLogic.UpdateNoticeInfo(model);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult QueryNoticeInfo([FromBody]QueryNoticeRequestModel model)
        {
            try
            {
                return _panguLogic.QueryNoticeInfo(model); 
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}