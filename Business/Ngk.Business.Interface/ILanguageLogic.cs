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
        /// ��ȡ��Ӧ������ʾ
        /// </summary>
        /// <param name="code"></param>
        /// <param name="enumLanguageDataType"></param>
        /// <returns></returns>
        string GetDescribe(string code, EnumLanguageDataType enumLanguageDataType);

        /// <summary>
        /// ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult InsertLanguage(LanguageDetailRequestModel model);

        /// <summary>
        /// ɾ
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ExcutedResult DeleteLanguage(string guid);

        /// <summary>
        /// ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult EditLanguage(LanguageDetailRequestModel model);

        /// <summary>
        /// ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryLanguage(LanguageQueryRequestModel model);
    }
}

