using System.Threading.Tasks;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface ICaptchaLogic : IBusinessLogic<Captcha>
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="countryCode">国际电话区号,发送国际短信为必传字段</param>
        /// <returns>执行结果</returns>
        Task<ExcutedResult<string>> SendCaptcha(string phoneNumber, string countryCode);

        /// <summary>
        /// 验证码是否有效
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="captcha">验证码</param>
        /// <param name="tempKey">临时Key</param>
        /// <returns>是否有效</returns>
        bool IsValidCaptcha(string phoneNumber, string captcha, string tempKey);
    }
}

