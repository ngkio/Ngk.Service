using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface ILanguageLogic : IBusinessLogic<Language>
    {
        /// <summary>
        /// 获取对应语言提示
        /// </summary>
        /// <param name="code"></param>
        /// <param name="enumLanguageDataType"></param>
        /// <returns></returns>
        string GetDescribe(string code, EnumLanguageDataType enumLanguageDataType);

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult InsertLanguage(LanguageDetailRequestModel model);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ExcutedResult DeleteLanguage(string guid);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult EditLanguage(LanguageDetailRequestModel model);

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryLanguage(LanguageQueryRequestModel model);
    }
}

