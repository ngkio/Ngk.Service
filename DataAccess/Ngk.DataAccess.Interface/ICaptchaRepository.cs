using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface ICaptchaRepository : IRepository<Captcha>
    {
        /// <summary>
        /// 检查验证码，若有小于一分钟之内的验证码，返回该记录，若有大于一分钟的进行删除
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="minutes">过期时间</param>
        /// <returns></returns>
        Captcha CheckMobileCaptcha(string mobile, int minutes = 5);
    }
}

