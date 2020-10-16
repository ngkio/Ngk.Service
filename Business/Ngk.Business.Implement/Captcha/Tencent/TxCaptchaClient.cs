using System.Net.Http;
using System.Threading.Tasks;
using Thor.Framework.Common.Helper.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Ngk.Business.Implement.Captcha.Tencent
{
    public class TxCaptchaClient : ICaptchaClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpContext _httpContext;

        private readonly string _appId;
        private readonly string _appSecret;

        private const string UrlFormat = "https://ssl.captcha.qq.com/ticket/verify?aid={0}&AppSecretKey={1}&Ticket={2}&Randstr={3}&UserIP={4}";


        public TxCaptchaClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _httpClientFactory = httpClientFactory;
            _httpContext = httpContext.HttpContext;

            _appId = configuration["Captcha:Tencent:AppId"];
            _appSecret = configuration["Captcha:Tencent:AppSecret"];
        }

        public async Task<CaptchaResult> Validate(string ticket,string randstr)
        {

            var userIp = _httpContext.GetRequestIp4Address();

            var requestUrl = string.Format(UrlFormat, _appId, _appSecret, ticket, randstr, userIp);

            var client = _httpClientFactory.CreateClient();
            var resultStr = await client.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<TxCaptchaResult>(resultStr);
        }
    }
}
