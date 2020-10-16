namespace Ngk.Business.Implement.Captcha
{
    public class CaptchaResult
    {
        public bool IsValid { get; set; }

        public string Level { get; set; }

        public string ErrorMessage { get; set; }
    }
}
