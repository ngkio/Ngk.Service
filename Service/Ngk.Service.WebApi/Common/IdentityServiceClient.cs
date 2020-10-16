using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Thor.Framework.Data.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Ngk.Service.WebApi.Common
{
    public class IdentityServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IdentityParams _identityParams;

        public IdentityServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;

            _identityParams = configuration.GetSection("IdentityService").Get<IdentityParams>();
        }

        public async Task<ExcutedResult<IdentityToken>> GetToken(string userId, string userInfo)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                ["client_id"] = _identityParams.ClientId,
                ["client_secret"] = _identityParams.ClientSecret,
                ["grant_type"] = "password",
                ["username"] = _identityParams.UserName,
                ["password"] = _identityParams.Password,
                ["UInfo"] = userInfo,
                ["UId"] = userId
            };

            var client = _httpClientFactory.CreateClient();
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await client.PostAsync($"{_identityParams.Uri}/connect/token", content);
                if (!msg.IsSuccessStatusCode)
                {
                    return ExcutedResult<IdentityToken>.FailedResult(((int) msg.StatusCode).ToString(),
                        "获取AccessToken失败");
                }

                return ExcutedResult<IdentityToken>.SuccessResult(await msg.Content.ReadAsAsync<IdentityToken>());
            }
        }
    }

    public class IdentityParams
    {
        public string Uri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class IdentityToken
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }

        [JsonProperty("expires_in")] public long ExpiresIn { get; set; }

        [JsonProperty("token_type")] public string TokenType { get; set; }
    }
}