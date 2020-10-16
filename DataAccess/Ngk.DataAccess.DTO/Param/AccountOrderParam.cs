using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountOrderParam:AdvQueryParam
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public String Account { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int BusinessState { get; set; }

        /// <summary>
        /// 完成时间开始
        /// </summary>
        public DateTime CompleteTimeStart { get; set; }

        /// <summary>
        /// 完成时间结束
        /// </summary>
        public DateTime CompleteTimeEnd { get; set; }
    }
}
