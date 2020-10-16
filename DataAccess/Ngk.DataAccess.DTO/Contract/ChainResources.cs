// Decompiled with JetBrains decompiler
// Type: EosSharp.Api.v1.TotalResources
// Assembly: EosSharp, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 1EC10CC0-1D7B-4F70-A3FC-49B899892EF3
// Assembly location: /Users/astaldo/.nuget/packages/eos-sharp/1.0.5/lib/netstandard2.0/EosSharp.dll

using System.Collections.Generic;

namespace Ngk.DataAccess.DTO.Contract
{
    public class ChainTotalResources
    {
        public string CpuWeight { get; set; }

        public string NetWeight { get; set; }

        public string Owner { get; set; }

        public ulong? RamBytes { get; set; }
    }
    
    public class ChainResource
    {
        public long Used { get; set; }

        public long Available { get; set; }

        public long Max { get; set; }
    }
    
    public class ChainRefundRequest
    {
        public string CpuAmount { get; set; }

        public string NetAmount { get; set; }
    }
    
    public class ChainSelfDelegatedBandwidth
    {
        public string CpuWeight { get; set; }

        public string From { get; set; }

        public string NetWeight { get; set; }

        public string To { get; set; }
    }
    
    public class ChainVoterInfo
    {
        public bool? IsProxy { get; set; }

        public double? LastVoteWeight { get; set; }

        public string Owner { get; set; }

        public List<string> Producers { get; set; }

        public double? ProxiedVoteWeight { get; set; }

        public string Proxy { get; set; }

        public ulong? Staked { get; set; }
    }
}
