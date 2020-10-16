using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Interface.Model
{
    /// <summary>
    /// 交易请求接口基本参数
    /// </summary>
    public class TransferBaseModel
    {

        /// <summary>
        /// 发起交易着
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 接收交易者
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string Contract { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Amount { get; set; }

        public string Fee { get; set; }

        public string Memo { get; set; }

        public string TransactionSign { get; set; }

        public override string ToString()
        {
            return $"Form:{From}  To:{To}-Amount:{Amount} {Symbol}";
        }
    }
}
