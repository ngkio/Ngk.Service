using System.Collections.Generic;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IConfigDataRepository : IRepository<ConfigData>
    {
        /// <summary>
        /// 根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetByKey(string key);

        /// <summary>
        /// 根据key集合获取配置数据集合
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        List<ConfigData> GetByKeyList(List<string> keyList);

        /// <summary>
        /// 根据key获取实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ConfigData GetEntityByKey(string key);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        void RefreshCache();
    }
}

