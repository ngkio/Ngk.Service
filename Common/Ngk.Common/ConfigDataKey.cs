using System;

namespace Ngk.Common
{
    public static class ConfigDataKey
    {
        /// <summary>
        /// 注册帐户限制数
        /// </summary>
        public static string RegisterAccountLimit = "RegisterAccountLimit";

        public static string AndroidDownUrl = "AndroidDownUrl";

        public static string IosDownUrl = "IOSDownUrl";

        /// <summary>
        /// 作弊登录开关
        /// </summary>
        public static string CheatSwitch = "CheatSwitch";

        /// <summary>
        /// 作弊登录手机号
        /// </summary>
        public static string CheatMobile = "CheatMobile";

        /// <summary>
        /// 作弊登录密码
        /// </summary>
        public static string CheatCode = "CheatCode";

        /// <summary>
        /// 邀请注册地址前缀
        /// </summary>
        public static string InviteRegistUrl = "InviteRegistUrl";

        /// <summary>
        /// 发送短信验证码短信模板
        /// </summary>
        public static string SmsTemplate = "SmsTemplate";

        public static string EnSmsTemplate = "EnSmsTemplate";

        /// <summary>
        /// 查询交易详情
        /// </summary>
        public static string SearchTxDetail = "SearchTxDetail";

        /// <summary>
        /// 创建链账号合约
        /// </summary>
        public static string CreateAccountContract = "CreateAccountContract";

        /// <summary>
        /// 创建账号的账号
        /// </summary>
        public static string CreateChainAccount = "CreateChainAccount";

        /// <summary>
        /// 创建链账号key
        /// </summary>
        public static string CreateChainAccountKey = "CreateChainAccountKey";

        /// <summary>
        /// 代理账号
        /// </summary>
        public static string AgentAccount = "AgentAccount";

        /// <summary>
        /// 代理账号Key
        /// </summary>
        public static string AgentAccountKey = "AgentAccountKey";

        /// <summary>
        /// 资源账号
        /// </summary>
        public static string ResourceAccount = "ResourceAccount";

        /// <summary>
        /// 资源账号Key
        /// </summary>
        public static string ResourceAccountKey = "ResourceAccountKey";

        /// <summary>
        /// 代理账号Key
        /// </summary>
        public static string AgentAccountPublicKey = "AgentAccountPublicKey";

        /// <summary>
        /// 拉黑合约过期小时数
        /// </summary>
        public static string ProposalExpireHour = "ProposalExpireHour";

        public static string IpWhiteList = "IpWhiteList";

        public static string TimestampOffsetMinute = "TimestampOffsetMinute";
    }
}
