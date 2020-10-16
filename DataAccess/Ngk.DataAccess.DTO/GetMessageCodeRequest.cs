using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class GetMessageCodeRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// 国家码
        /// </summary>
        [Required]
        public string CountryCode { get; set; }

        /// <summary>
        /// 图形验证码票据
        /// </summary>
        [Required]
        public string Ticket { get; set; }

        /// <summary>
        /// 随机字符
        /// </summary>
        [Required]
        public string RandStr { get; set; }
    }
}
