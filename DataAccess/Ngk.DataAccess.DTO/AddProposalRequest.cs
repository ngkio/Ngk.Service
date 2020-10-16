using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ngk.DataAccess.DTO
{
    public class AddProposalRequest
    {
        /// <summary>
        /// 发起人
        /// </summary>
        [Required]
        public string Initiator { get; set; }

        /// <summary>
        /// 提案目标
        /// </summary>
        [Required]
        public string Target { get; set; }

        /// <summary>
        /// 提案描述
        /// </summary>
        [Required]
        public string Desc { get; set; }
    }

    public class UpdateProposalRequest
    {
        /// <summary>
        /// 提案ID
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 交易HASH
        /// </summary>
        [Required]
        public string TxHash { get; set; }
    }
}
