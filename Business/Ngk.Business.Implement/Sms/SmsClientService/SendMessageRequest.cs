using System.ComponentModel.DataAnnotations;
using Thor.Framework.Service.Signature;

namespace Ngk.Business.Implement.Sms.SmsClientService
{
    /// <summary>
    /// 
    /// </summary>
    public class SendMessageRequest: IValidateSignature
    {
        /// <summary>
        /// 【签名（顺序-0）】调用方Key(验证字段)
        /// </summary>
        [Required]
        [ValidateSignatureProperty("key", 0)]
        public string Key { get; set; }

        /// <summary>
        /// 【签名（顺序-1）】 模板编码
        /// </summary>
        [Required]
        [ValidateSignatureProperty("templateCode", 1)]

        public string TemplateCode { get; set; }
        /// <summary>
        /// 【签名（顺序-2）】 手机号
        /// </summary>
        [Required]
        [ValidateSignatureProperty("mobile", 2)]
        public string Mobile { get; set; }

        /// <summary>
        /// 【签名（顺序-3）】传入参数
        /// </summary>
        [Required]
        [ValidateSignatureProperty("jsonCode", 3)]
        public string JsonCode { get; set; }
        /// <summary>
        /// 【签名（顺序-4）】客户端 web 1,IOS 2,安卓 3
        /// </summary>
        [Required]
        [ValidateSignatureProperty("client", 4)]
        public int Client { get; set; }

        /// <summary>
        /// 国家代码 默认”86” 中国大陆国际区号
        /// </summary>
        public string CountryNumber { get; set; }

        /// <summary>
        /// 签名，签名规则另外说明(验证字段)
        /// </summary>
        [Required]
        public string Signature { get; set; }

    }
}
