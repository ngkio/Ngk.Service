using System.Collections.Generic;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IBlackContractRepository : IRepository<BlackContract>
    {
        List<BlackContract> GetProposalList();

        /// <summary>
        /// 是否为重复目标
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        bool CheckRepeatTarget(string target);

        BlackContract GetProposalDetail(long contractId);
    }
}

