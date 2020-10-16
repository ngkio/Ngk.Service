namespace Ngk.Common.Enum
{
    public enum EnumPayType
    {
        /// <summary>
        /// 合约支付
        /// </summary>
        Contract = 1,

        /// <summary>
        /// 好友支付
        /// </summary>
        FriendPay = 2,

        /// <summary>
        /// 支付宝
        /// </summary>
        AliPay = 3,

        //WeiXin = 4,

        /// <summary>
        /// 支付系统，支付宝-11
        /// </summary>
        PaySystemAliPay = 11,

        /// <summary>
        /// 支付系统，微信-12
        /// </summary>
        PaySystemWeiXin = 12,
    }
}
