using System.Collections.Generic;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.Service.WebApi.Attribute;

namespace Ngk.Service.WebApi.Controllers
{
    /// <summary>
    /// Token控制器
    /// </summary>
    [Log]
    [Language]
    [Route("token/[action]")]
    public class TokenController : Controller
    {
        private readonly ITokensLogic _tokensLogic;

        public TokenController(ITokensLogic tokensLogic)
        {
            _tokensLogic = tokensLogic;
        }

        /// <summary>
        /// 获取币种列表,链Code统一传ngk
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ExcutedResult<List<GetTokenListResponse>> GetTokenList([FromBody] ChainModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ExcutedResult<List<GetTokenListResponse>>.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                }
                var result = _tokensLogic.GetTokenList(model);
                return ExcutedResult<List<GetTokenListResponse>>.SuccessResult(result);
            }
            catch (BusinessException bex)
            {
                return ExcutedResult<List<GetTokenListResponse>>.FailedResult(bex.ErrorCode, bex.Message);
            }
        }
    }
}
