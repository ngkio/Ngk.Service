using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class TransferRecordParam : AdvQueryParam
    {
        /// <summary>
        /// 帐号名,
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 链Code
        /// </summary>
        public string ChainCode { get; set; }

        /// <summary>
        /// 使用合约
        /// </summary>
        public String Contract { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        public String TransferId { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public String TokenCode { get; set; }
    }
}
