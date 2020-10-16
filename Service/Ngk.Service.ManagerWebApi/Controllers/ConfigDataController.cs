using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.Service.ManagerWebApi.Attribute;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    /// <summary>
    /// 配置表
    /// </summary>
    [Language]
    public class ConfigDataController : Controller
    {
        private readonly IConfigDataLogic _configDataLogic;

        public ConfigDataController(IConfigDataLogic configDataLogic)
        {
            _configDataLogic = configDataLogic;

        }

        [HttpGet]
        public ExcutedResult GetQuery(string configKey, int pageIndex = 1, int pageSize = 10, string sortName = "", bool? order = null)
        {
            try
            {
                ConfigDataParam param = new ConfigDataParam()
                {
                    ConfigKey = configKey,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    SortName = "CreateTime",
                    IsSortOrderDesc = true
                };

                if (!string.IsNullOrEmpty(sortName))
                {
                    param.SortName = sortName;
                }
                if (order.HasValue)
                {
                    param.IsSortOrderDesc = order.Value;
                }

                var result = _configDataLogic.GetCconfigListQuery(param);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Update([FromBody] ConfigData modle)
        {
            try
            {
                _configDataLogic.Update(modle, out var result);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ExcutedResult RefreshMemoryCache()
        {
            try
            {
                _configDataLogic.RefreshMemoryCache();
                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}