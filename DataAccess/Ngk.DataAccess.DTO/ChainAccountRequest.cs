using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class ChainAccountRequest
    {
        /// <summary>
        /// 帐号名
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 链Code
        /// </summary>
        [Required]
        public string ChainCode { get; set; }
    }

    public class TokenAccountRequest : ChainAccountRequest
    {
        /// <summary>
        /// Token Code
        /// </summary>
        [Required]
        public string Token { get; set; }
    }
}
