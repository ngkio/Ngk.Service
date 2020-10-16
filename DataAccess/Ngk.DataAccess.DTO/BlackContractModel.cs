using System;
using System.Collections.Generic;
using System.Text;

namespace Ngk.DataAccess.DTO
{
    public class BlackContractModel
    {
        /// <summary>
        /// 提案ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 合约序号（平台使用）
        /// </summary>
        public long ContractId { get; set; }

        /// <summary>
        /// 提案发起人
        /// </summary>
        public string Initiator { get; set; }

        /// <summary>
        /// 提案目标合约
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 提案描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 提案过期时间（UTC）
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 提案过期时间戳
        /// </summary>
        public long ExpireTimestamp { get; set; }

        /// <summary>
        /// 交易Hash
        /// </summary>
        public string TransferId { get; set; }

        /// <summary>
        /// 提案状态：1、进行中2、通过3、驳回
        /// </summary>
        public int ProposalState { get; set; }
    }
}
