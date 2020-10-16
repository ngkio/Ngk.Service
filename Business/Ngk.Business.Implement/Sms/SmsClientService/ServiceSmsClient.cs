using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Thor.Framework.Service.Signature;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ngk.Common.Enum;

namespace Ngk.Business.Implement.Sms.SmsClientService
{
    public class ServiceSmsClient : SmsClient
    {
        /// <summary>
        /// 调用方Key(验证字段)
        /// </summary>
        private readonly String _key;

        /// <summary>
        /// 加密字段
        /// </summary>
        private readonly String _securityKey;

        /// <summary>
        /// 链接地址
        /// </summary>
        private readonly String _url;


        public override string Name => "SmsClient";


        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceSmsClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _key = configuration["SMS:Key"];
            _securityKey = configuration["SMS:SecurityKey"];
            _url = configuration["SMS:Url"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="content"></param>
        /// <param name="clientType"></param>
        /// <param name="countryNumber"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        protected override async Task<SendResult> Send(string phoneNumber, string content, int clientType = 1, string countryNumber = "", string templateCode = "")
        {
            SendResult sendResult = new SendResult();
            SendMessageRequest model = new SendMessageRequest();
            model.Key = _key;
            model.Client = (int)EnumClientType.Web;
            model.TemplateCode = templateCode;
            model.Mobile = phoneNumber;
            model.JsonCode = content;
            model.CountryNumber = countryNumber;
            String signa = SignHelper.GetGenSign(model, _securityKey);
            model.Signature = signa;
            var client = _httpClientFactory.CreateClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage resp = await client.PostAsync(_url, httpContent);
            resp.EnsureSuccessStatusCode();
            string result = await resp.Content.ReadAsStringAsync();

            sendResult.Content = content;
            sendResult.PhoneNumber = phoneNumber;
            if (!String.IsNullOrEmpty(result))
            {
                sendResult.ResponseStr = result;
                ExcutedResult excutedResult = JsonConvert.DeserializeObject<ExcutedResult>(result);
                if (excutedResult.Code == SysResultCode.Success)
                {
                    sendResult.IsSuccess = true;
                }
                else
                {
                    sendResult.IsSuccess = false
                        ;
                }
            }
            return sendResult;
        }

        /// <summary>
        /// 获取短信内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected override string GetSmsContent(string code)
        {
            var data = new
            {
                code = code
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}
