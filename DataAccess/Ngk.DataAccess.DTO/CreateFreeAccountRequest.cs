using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 创建帐户请求模型
    /// </summary>
    public class CreateFreeAccountRequest
    {
        /// <summary>
        /// 帐号名
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 公钥,必传
        /// </summary>
        [Required]
        public string PublicKey { get; set; }
    }
}
