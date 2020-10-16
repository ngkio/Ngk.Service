using System.Collections.Generic;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Pager;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IConfigDataLogic : IBusinessLogic<ConfigData>
    {
        /// <summary>
        /// 根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetByKey(string key);

        /// <summary>
        /// 使用语言获取数据 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetByKeyAndLang(string key);

        /// <summary>
        /// 根据key集合获取配置数据集合
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        List<ConfigData> GetByKeyList(List<string> keyList);

        /// <summary>
        /// 查询-后端
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IList<ConfigData> GetByParam(string key);
        
        /// <summary>
        /// 获取配置列表(分页)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResults<ConfigData> GetCconfigListQuery(ConfigDataParam param);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        void RefreshMemoryCache();
    }
}

