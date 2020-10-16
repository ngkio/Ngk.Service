using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class AccountOrderRequest
    {
        /// <summary>
        /// 链Code
        /// </summary>
        [Required]
        public string ChainCode { get; set; }

        /// <summary>
        /// 帐号名
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Owner权限公钥
        /// </summary>
        public string OwnerKey { get; set; }

        /// <summary>
        /// Active权限公钥
        /// </summary>
        public string ActiveKey { get; set; }

        /// <summary>
        /// 支付类型1、合约转帐、2、好友代付,3、支付宝快捷支付
        /// </summary>
        public int PayType { get; set; }
    }
}
