using System;
using System.Collections.Generic;
using System.Linq;
using Ngk.Common.Enum;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class BlackContractRepository : BaseRepository<BlackContract>, IBlackContractRepository
    {
        public BlackContractRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public List<BlackContract> GetProposalList()
        {
            var query = DbSet.Where(p =>
                p.State == (int)EnumState.Normal && p.ProposalState == (int)EnumProposalState.Normal &&
                p.ExpireTime > DateTime.UtcNow);
            return query.ToList();
        }

        /// <summary>
        /// 是否为重复目标
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool CheckRepeatTarget(string target)
        {
            var isRepeat = DbSet.Any(p => p.State == (int)EnumState.Normal && p.ProposalState == (int)EnumProposalState.Normal &&
                           p.ExpireTime > DateTime.UtcNow && p.Target == target);
            return isRepeat;
        }

        public BlackContract GetProposalDetail(long contractId)
        {
            var entity = DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.ContractId == contractId);
            return entity;
        }
    }
}


