using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.DTO
{
    public class GetTokenListResponse
    {
        /// <summary>
        /// 币种名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 币种Symbol
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 币种合约
        /// </summary>
        public string Contract { get; set; }

        /// <summary>
        /// Logo地址
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 是否为系统币种
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 官网
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 白皮书地址
        /// </summary>
        public string WhitePaperUrl { get; set; }

        /// <summary>
        /// Facebook联系地址
        /// </summary>
        public string FaceBookUrl { get; set; }

        /// <summary>
        /// Twitter联系地址
        /// </summary>
        public string TwitterUrl { get; set; }

        /// <summary>
        /// 发行状态
        /// </summary>
        public string IssueState { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 发行费用
        /// </summary>
        public string IssueCost { get; set; }

        /// <summary>
        /// 人民币市场行情
        /// </summary>
        public decimal RmbPrice { get; set; }

        /// <summary>
        /// 美元市场行情
        /// </summary>
        public decimal DollarPrice { get; set; }

        public int Decimals { get; set; }

        public int Precision { get; set; }
    }
}
