using System.Collections.Generic;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IChainConfigRepository : IRepository<ChainConfig>
    {
        /// <summary>
        /// 获取全部可用链
        /// </summary>
        /// <returns></returns>
        List<ChainConfig> GetAll();
        
        /// <summary>
        /// 按Code获取链
        /// </summary>
        /// <returns></returns>
        ChainConfig GetByChainCode(string chainCode);
    }
}

