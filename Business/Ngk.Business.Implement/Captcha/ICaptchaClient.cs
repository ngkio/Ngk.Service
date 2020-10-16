using System.Threading.Tasks;

namespace Ngk.Business.Implement.Captcha
{
    public interface ICaptchaClient
    {
        Task<CaptchaResult> Validate(string ticket,string randstr);
    }
}
