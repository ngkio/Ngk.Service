using System;
using System.Collections.Generic;
using System.Text;

namespace Ngk.DataAccess.DTO
{
    public class ProposerResponse
    {
        /// <summary>
        /// 提案ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 合约顺序
        /// </summary>
        public long ContractId { get; set; }

        /// <summary>
        /// 过期时间戳
        /// </summary>
        public long ExpireTimestamp { get; set; }
    }
}
