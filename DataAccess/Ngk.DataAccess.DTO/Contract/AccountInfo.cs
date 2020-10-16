using System;
using System.Collections.Generic;

namespace Ngk.DataAccess.DTO.Contract
{
    public class AccountInfo
    {
        public string AccountName { get; set; }

        public uint? HeadBlockNum { get; set; }

        public DateTime? HeadBlockTime { get; set; }

        public bool? Privileged { get; set; }

        public DateTime? LastCodeUpdate { get; set; }

        public DateTime? Created { get; set; }

        public long? RamQuota { get; set; }

        public long? NetWeight { get; set; }

        public long? CpuWeight { get; set; }

        //        public Resource NetLimit { get; set; }
        //
        //        public Resource CpuLimit { get; set; }

        public ulong? RamUsage { get; set; }

        public List<ChainPermission> Permissions { get; set; }

        //        public RefundRequest RefundRequest { get; set; }

        //        public SelfDelegatedBandwidth SelfDelegatedBandwidth { get; set; }

        public ChainTotalResources ChainTotalResources { get; set; }

        //        public VoterInfo VoterInfo { get; set; }
    }
}
