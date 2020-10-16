using System.Collections.Generic;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;

namespace Ngk.Business.Interface
{
    public interface IBlackContractLogic : IBusinessLogic<BlackContract>
    {
        /// <summary>
        /// 添加提案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ProposerResponse AddProposal(AddProposalRequest request);

        /// <summary>
        /// 更新提案Hash
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExcutedResult UpdateProposalHash(UpdateProposalRequest request);

        List<BlackContractModel> GetProposalList();

        BlackContractModel GetProposalDetail(long contractId);
    }
}

