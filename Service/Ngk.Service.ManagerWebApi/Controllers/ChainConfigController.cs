using System.Linq;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Service.ManagerWebApi.Attribute;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Language]
    public class ChainConfigController : Controller
    {
        private readonly IChainConfigLogic _chainConfigLogic;

        public ChainConfigController(IChainConfigLogic chainConfigLogic)
        {
            _chainConfigLogic = chainConfigLogic;

        }

        /// <summary>
        /// 获取可用链列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult GetChainList()
        {
            try
            {
                var list = _chainConfigLogic.GetAll().Select(x =>
                    new {
                        x.ChainCode,
                    }
                );

                return ExcutedResult.SuccessResult(list);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}