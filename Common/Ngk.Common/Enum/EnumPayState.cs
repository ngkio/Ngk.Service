namespace Ngk.Common.Enum
{
    public enum EnumPayState
    {
        /// <summary>
        /// 待支付
        /// </summary>
        WaitForPay = 1,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// 已支付
        /// </summary>
        Payed = 3,

        /// <summary>
        /// 已退款
        /// </summary>
        Refund = 4,
    }
}
