using System;

namespace Ngk.DataAccess.DTO
{
   public  class ApplyDappAuditRequest
    {
        public Guid? DappId { get; set; }

        public string DappNameZh { get; set; }

        public string DappNameEn { get; set; }

        public string SmartContracts { get; set; }

        public string Website { get; set; }

        public Guid? DappLogo { get; set; }

        public string TeamDesc { get; set; }

        public string ProjectDesc { get; set; }

        public string DappDesc { get; set; }

        public string DappTag { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string ProjectWechat { get; set; }

        public string ServerWechat { get; set; }

        public bool IsHaveWechatUserGroup { get; set; }

        public bool IsRebate { get; set; }

        public decimal? RebateRate { get; set; }

        public string RebateToken { get; set; }

        public string RebateAccount { get; set; }

        public string RebateUrl { get; set; }

        public int? RebateType { get; set; }

        public bool? IsNeedActiveToRebate { get; set; }

        public bool IsPublishToken { get; set; }

        public string TokenName { get; set; }

        public string Symbol { get; set; }

        public string ChainCode { get; set; }

        public string TokenContract { get; set; }

        public Guid? TokenLogo { get; set; }

        public int Precision { get; set; }

        public string TokenDesc { get; set; }

        public string TokenTag { get; set; }
    }
}
