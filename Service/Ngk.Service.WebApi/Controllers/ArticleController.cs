using System;
using Microsoft.AspNetCore.Http;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.Service.WebApi.Attribute;

namespace Ngk.Service.WebApi.Controllers
{
    /// <summary>
    /// 文章控制器
    /// </summary>
    //[Log]
    [Language]
    [Route("article/[action]")]
    public class ArticleController : Controller
    {
        private readonly IArticleLogic _articleLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleController(IArticleLogic articleLogic, IHttpContextAccessor httpContextAccessor)
        {
            _articleLogic = articleLogic;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 按code获取文章，目前包含UserProtocol（使用协议）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult<ArticleModel> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return ExcutedResult<ArticleModel>.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
            }
            var langType = LanguageHelper.GetEnumLanguageType(_httpContextAccessor.HttpContext);
            string newCode = "";
            if (langType != EnumLanguageType.Zh)
            {
                newCode = $"{code}_{langType.ToString().ToLower()}";
            }
            var entity = _articleLogic.GetByCode(newCode);
            if (entity == null)
            {
                entity = _articleLogic.GetByCode(code);
            }
            if (entity != null)
            {
                var obj = new ArticleModel
                {
                    Code = entity.Code,
                    Title = entity.Title,
                    Content = entity.Content,
                };
                return ExcutedResult<ArticleModel>.SuccessResult(obj);
            }
            return ExcutedResult<ArticleModel>.SuccessResult();
        }
    }
}
