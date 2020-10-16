using System;
using System.Threading.Tasks;
using YunPian.Services;

namespace Ngk.Business.Implement.Sms
{
    public class YunPianSmsClient : SmsClient
    {
        public override string Name => "YunPian";

        public readonly ISmsService _smsService;

        public YunPianSmsClient(ISmsService smsService)
        {
            _smsService = smsService;
        }

        protected override Task<SendResult> Send(string phoneNumber, string content, int clientType = 1, string countryNumber = "", string templateCode = "")
        {
            // 发送单条短信
            var result = _smsService.SingleSendAsync(content, countryNumber + phoneNumber);
            result.Wait();
            return Task.FromResult(new SendResult
            {
                Content = content,
                IsSuccess = result.Result.IsSuccess(),
                PhoneNumber = phoneNumber,
                ResponseStr = result.Result.Msg
            });
        }

        protected override string GetSmsContent(string code)
        {
            return code;
        }
    }
}
