namespace Ngk.Common.Enum
{
    public enum EnumOrderState
    {
        /// <summary>
        /// 待支付
        /// </summary>
        WaitForPay = 1,

        /// <summary>
        /// 取消
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// 已支付
        /// </summary>
        Payed = 3,

        /// <summary>
        /// 退款
        /// </summary>
        Refund = 4,

        /// <summary>
        /// 完成订单
        /// </summary>
        Completed = 5,

        /// <summary>
        /// 过期
        /// </summary>
        Expired = 9,
    }
}
