using System;
using System.Threading.Tasks;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Ngk.Common;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement.Sms
{
    public abstract class SmsClient
    {
        public abstract string Name { get; }

        protected abstract Task<SendResult> Send(string phoneNumber, string content, int clientType = 1, String countryNumber = "", string templateCode = "");

        /// <summary>
        /// 获取短信的内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected abstract String GetSmsContent(String code);


        /// <summary>
        /// 获取短信的内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public String SmsContent(String code)
        {
            return GetSmsContent(code);
        }

        public async Task<ExcutedResult> SendAsync(string phoneNumber, string content, int client, String countryNumber = "", string templateCode = "")
        {
            var result = await Send(phoneNumber, content, client, countryNumber, templateCode);
            IHttpContextAccessor httpContextAccessor = AspectCoreContainer.Resolve<IHttpContextAccessor>();
            var clientIp = httpContextAccessor.HttpContext.GetRequestIp4Address().ToString();

            var smsSend = new SmsSend
            {
                Id = Guid.NewGuid(),
                Mobile = result.PhoneNumber,
                Content = result.Content,
                CreateTime = DateTime.UtcNow,
                SmsProxy = Name,
                SmsProxyResp = result.ResponseStr,
                IsSuccess = result.IsSuccess,
                Client = client,
                LoginIp = clientIp
            };

            ISmsSendRepository smsSendRepository = AspectCoreContainer.Resolve<ISmsSendRepository>();
            smsSendRepository.Insert(smsSend);

            if (result.IsSuccess)
            {
                return ExcutedResult.SuccessResult();
            }

            return ExcutedResult.FailedResult(BusinessResultCode.SmsSendFail, "短信发送失败");
        }


        protected class SendResult
        {
            public string PhoneNumber { get; set; }
            public string Content { get; set; }
            public string ResponseStr { get; set; }
            public bool IsSuccess { get; set; }
        }
    }
}