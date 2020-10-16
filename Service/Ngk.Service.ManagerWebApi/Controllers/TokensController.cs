using System;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Manager;
using Ngk.DataAccess.DTO.Param;
using Ngk.Service.ManagerWebApi.Attribute;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    /// <summary>
    /// Token控制器
    /// </summary>
    [Language]
    public class TokensController : Controller
    {
        private readonly ITokensLogic _tokensLogic;


        public TokensController(ITokensLogic tokensLogic)
        {
            _tokensLogic = tokensLogic;
        }

        /// <summary>
        /// 查询Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult Query(string name, string tag, int pageIndex = 1, int pagesize = 10, string sortName = "", bool? order = null)
        {
            try
            {
                TokenParam param = new TokenParam()
                {
                    Name = name,
                    PageIndex = pageIndex,
                    PageSize = pagesize,
                    SortName = "Order",
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
                var result = _tokensLogic.AdvQuery(param);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 添加Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Add([FromBody]TokenCreateModel model)
        {
            try
            {
                var result = _tokensLogic.AddToken(model);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 编辑Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Edit([FromBody]TokenEditModel model)
        {
            try
            {
                var result = _tokensLogic.Edit(model);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Delete(Guid id)
        {
            try
            {
                _tokensLogic.Delete(id, out var result);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}