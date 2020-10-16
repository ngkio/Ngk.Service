using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class ChainConfigRepository : BaseRepository<ChainConfig>, IChainConfigRepository
    {
        public ChainConfigRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 获取全部可用链
        /// </summary>
        /// <returns></returns>
        public List<ChainConfig> GetAll()
        {
            var list = DbSet.Where(p => p.State == (int)EnumState.Normal);
            return list.ToList();
        }

        /// <summary>
        /// 按Code获取链
        /// </summary>
        /// <returns></returns>
        public ChainConfig GetByChainCode(string chainCode)
        {
            chainCode = chainCode.ToUpper();
            var entity = DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.ChainCode == chainCode);
            return entity;
        }
    }
}


