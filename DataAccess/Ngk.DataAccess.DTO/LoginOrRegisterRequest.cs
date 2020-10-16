using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class LoginOrRegisterRequest : MobileCodeRequest
    {
        /// <summary>
        /// 设备号
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 代理
        /// </summary>
        public string Agent { get; set; }

        public string Ip { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { get; set; }
    }

    public class MobileCodeRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 临时Key
        /// </summary>
        public string TempKey { get; set; }
    }
}
