namespace Ngk.Common
{
    public class BusinessResultCode
    {
        /// <summary>
        /// 参数错误或无效,1001
        /// </summary>
        public const string ArgumentError = "1001";

        /// <summary>
        /// 签名错误,1004
        /// </summary>
        public const string SignError = "1004";

        /// <summary>
        /// 没有签名,1005
        /// </summary>
        public const string NoSign = "1005";

        /// <summary>
        /// 帐号名不符合规则,1102
        /// </summary>
        public const string AccountNotInRule = "1102";

        /// <summary>
        /// 帐号已存在,1103
        /// </summary>
        public const string AccountExist = "1103";

        /// <summary>
        /// 创建帐号失败,1104
        /// </summary>
        public const string CreateAccountFail = "1104";

        /// <summary>
        /// 公钥格式错误,1109
        /// </summary>
        public const string PublicKeyError = "1109";

        /// <summary>
        /// 文件保存失败
        /// </summary>
        public const string FileSaveFail = "1201";

        /// <summary>
        /// 文件超过2M
        /// </summary>
        public const string FileSizeThan2M = "1202";

        /// <summary>
        /// 文件不存在
        /// </summary>
        public const string FileNotExistYet = "1211";

        /// <summary>
        /// 短信发送失败
        /// </summary>
        public const string SmsSendFail = "1410";

        /// <summary>
        /// 版本错误
        /// </summary>
        public const string VersionError = "1605";

        /// <summary>
        /// 当前版本号小于历史版本号
        /// </summary>
        public const string VersionIsSmallError = "1607";

        /// <summary>
        /// 用户不存在
        /// </summary>
        public const string NoUser = "1614";
        
        /// <summary>
        /// 用户不存在或密码错误
        /// </summary>
        public const string NoUserOrPasswordError = "1625";

        /// <summary>
        /// 提交TOKEN错误
        /// </summary>
        public const string SubmitTokenError = "1630";

        /// <summary>
        /// 操作失败
        /// </summary>
        public const string OperationFailed = "1634";

        /// <summary>
        /// 数据不存在，请刷新!
        /// </summary>
        public const string DataNotExist = "1639";

        /// <summary>
        /// 数据已存在
        /// </summary>
        public const string DataExist = "1640";

        /// <summary>
        /// Chain request api error.
        /// </summary>
        public const string ChainRequestApiError = "1643";

        /// <summary>
        /// EOS request  error.
        /// </summary>
        public const string ChainRequestError = "1644";

        /// <summary>
        /// 该账号已被锁定，请稍后再试!
        /// </summary>
        public const string AccountLockedTryLater = "1645";

        /// <summary>
        /// 您已无法创建NGK账号，请联系客服人员
        /// </summary>
        public const string FreeAccountLimit = "1658";
        
        /// <summary>
        /// 该合约正在提案中...  1660
        /// </summary>
        public const string ProposalRepeat = "1660";
    }
}