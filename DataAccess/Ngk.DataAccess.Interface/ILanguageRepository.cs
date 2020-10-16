using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface ILanguageRepository : IRepository<Language>
    {

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="enumLanguageDataType">Language中Type的类型</param>
        /// <param name="enumLanguageType">需要什么样的语言类型</param>
        /// <returns></returns>
        string GetDescribe(string code, EnumLanguageDataType enumLanguageDataType, EnumLanguageType enumLanguageType);


        /// <summary>
        /// 根据类型和code获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Language GetLanguageByTypeAndCode(string code, int type);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResults<Language> QueryLanguage(LanguageParam model);
    }
}

