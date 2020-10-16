using Microsoft.Extensions.Configuration;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;

namespace Ngk.Business.Implement
{
    public class BlackContractLogic : BaseBusinessLogic<BlackContract, IBlackContractRepository>, IBlackContractLogic
    {
        private readonly IConfigDataLogic _configDataLogic;
        private readonly IRandomIdRepository _randomIdRepository;

        #region ctor

        public BlackContractLogic(IBlackContractRepository repository, IRandomIdRepository randomIdRepository, IConfigDataLogic configDataLogic) : base(repository)
        {
            _randomIdRepository = randomIdRepository;
            _configDataLogic = configDataLogic;
        }

        #endregion


        public ProposerResponse AddProposal(AddProposalRequest request)
        {
            if (Repository.CheckRepeatTarget(request.Target))
            {
                throw new BusinessException(BusinessResultCode.ProposalRepeat, "该合约正在提案中...");
            }
            var hour = _configDataLogic.GetByKey(ConfigDataKey.ProposalExpireHour);
            var iHour = int.Parse(hour);
            var time = DateTime.UtcNow.AddHours(iHour);
            BlackContract entity = new BlackContract()
            {
                ContractId = _randomIdRepository.GetContractNum(),
                Desc = request.Desc,
                ExpireTime = time,
                ExpireTimestamp = time.ToTimestamp(),
                CreateTime = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Initiator = request.Initiator,
                ProposalState = (int)EnumProposalState.Add,
                State = (int)EnumState.Normal,
                Target = request.Target,
                TransferId = "",
            };
            Create(entity, out var result);
            ProposerResponse response = new ProposerResponse()
            {
                Id = entity.Id,
                ContractId = entity.ContractId,
                ExpireTimestamp = entity.ExpireTimestamp
            };
            return response;
        }

        public ExcutedResult UpdateProposalHash(UpdateProposalRequest request)
        {
            var entity = GetById(request.Id);
            if (entity == null)
                return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "未找到相关提案");
            entity.TransferId = request.TxHash;
            entity.ProposalState = (int)EnumProposalState.Normal;
            Update(entity, out var result);
            return result;
        }

        public List<BlackContractModel> GetProposalList()
        {
            var list = Repository.GetProposalList();
            var result = list.Select(p => new BlackContractModel()
            {
                ContractId = p.ContractId,
                Desc = p.Desc,
                ExpireTime = p.ExpireTime,
                ExpireTimestamp = p.ExpireTimestamp,
                Id = p.Id,
                Initiator = p.Initiator,
                ProposalState = p.ProposalState,
                Target = p.Target,
                TransferId = p.TransferId
            }).ToList();
            return result;
        }

        public BlackContractModel GetProposalDetail(long contractId)
        {
            var entity = Repository.GetProposalDetail(contractId);
            if (entity == null)
                return null;
            var model = new BlackContractModel()
            {
                ContractId = entity.ContractId,
                Desc = entity.Desc,
                ExpireTime = entity.ExpireTime,
                ExpireTimestamp = entity.ExpireTimestamp,
                Id = entity.Id,
                Initiator = entity.Initiator,
                ProposalState = entity.ProposalState,
                Target = entity.Target,
                TransferId = entity.TransferId
            };
            return model;
        }
    }
}