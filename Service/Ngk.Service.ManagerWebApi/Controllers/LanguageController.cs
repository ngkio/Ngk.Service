using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.DTO;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    public class LanguageController : Controller
    {
        private static ILanguageLogic _LanguageLogic;

        public LanguageController(ILanguageLogic languageLogic)
        {
            _LanguageLogic = languageLogic;
        }

        /// <summary>
        /// Language增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult InsertLanguage([FromBody]LanguageDetailRequestModel model)
        {
            return _LanguageLogic.InsertLanguage(model);
        }

        /// <summary>
        /// Language删
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult DeleteLanguage(string guid)
        {
            return _LanguageLogic.DeleteLanguage(guid);
        }

        /// <summary>
        /// Language改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult EditLanguage([FromBody]LanguageDetailRequestModel model)
        {
            return _LanguageLogic.EditLanguage(model);
        }

        /// <summary>
        /// Language查
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult QueryLanguage([FromBody]LanguageQueryRequestModel model)
        {
            return _LanguageLogic.QueryLanguage(model);
        }
    }
}