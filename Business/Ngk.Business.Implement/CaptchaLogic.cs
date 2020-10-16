using System;
using System.Linq;
using System.Threading.Tasks;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Microsoft.Extensions.Configuration;
using Ngk.Business.Implement.Sms;
using Ngk.Business.Implement.Sms.SmsClientService;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class CaptchaLogic : BaseBusinessLogic<DataAccess.Entities.Captcha, ICaptchaRepository>, ICaptchaLogic
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigDataLogic _configDataLogic;

        #region ctor

        public CaptchaLogic(ICaptchaRepository repository, IConfiguration configuration, IConfigDataLogic configDataLogic) : base(repository)
        {
            _configuration = configuration;
            _configDataLogic = configDataLogic;
        }

        #endregion

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="countryCode">国际电话区号,发送国际短信为必传字段</param>
        /// <returns>执行结果</returns>
        public async Task<ExcutedResult<string>> SendCaptcha(string phoneNumber, string countryCode)
        {
            try
            {
                var oldCaptcha = Repository.CheckMobileCaptcha(phoneNumber);
                if (oldCaptcha != null)
                    return ExcutedResult<string>.SuccessResult("", oldCaptcha.Id.ToString());
                IConfiguration config = AspectCoreContainer.Resolve<IConfiguration>();
                var expireSeconds = int.Parse(config["Captcha:Common:ExpireSeconds"]);
                var captcha = CodeHelper.CreateMobileCode(phoneNumber);

                var entity = new DataAccess.Entities.Captcha
                {
                    Id = Guid.NewGuid(),
                    Code = captcha,
                    CountryCode = countryCode,
                    ExpireTime = DateTime.UtcNow.AddSeconds(expireSeconds),
                    Type = (int)EnumCaptchaType.Sms,
                    CreateTime = DateTime.UtcNow,
                    Mobile = phoneNumber,
                    State = (int)EnumState.Normal
                };
                Create(entity, out var excutedResult);
                if (excutedResult.Status != EnumStatus.Success)
                {
                    return ExcutedResult<string>.FailedResult(excutedResult.Code, excutedResult.Message);
                }

                #region 新版短信
                //短信模板
                var templateCode = "";
                if (!countryCode.Contains("86"))
                {
                    templateCode = _configDataLogic.GetByKey(ConfigDataKey.EnSmsTemplate);
                }
                else
                {
                    templateCode = _configDataLogic.GetByKey(ConfigDataKey.SmsTemplate);
                }
                //短信渠道
                SmsClient smsClient = AspectCoreContainer.Resolve<YunPianSmsClient>();
                string content = string.Format(templateCode, captcha);
                int client = 1;//固定传web
                excutedResult = await smsClient.SendAsync(phoneNumber, content, client, countryCode, templateCode);
                if (excutedResult.Status != EnumStatus.Success || excutedResult.Code != SysResultCode.Success)
                {
                    return ExcutedResult<string>.FailedResult(excutedResult.Code, excutedResult.Message);
                }
                #endregion
                return ExcutedResult<string>.SuccessResult("", entity.Id.ToString());
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, "发送验证码错误");
                throw;
            }
        }

        /// <summary>
        /// 验证码是否有效
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="captcha">验证码</param>
        /// <param name="tempKey">临时Key</param>
        /// <returns>是否有效</returns>
        public bool IsValidCaptcha(string phoneNumber, string captcha, string tempKey)
        {
            if (CheckBackDoor(phoneNumber, captcha))
                return true;
            var data = Repository.FirstOrDefault(p => p.Mobile == phoneNumber && p.Code == captcha);
            if (data == null)
            {
                return false;
            }
            if (data.Id.ToString() != tempKey)
            {
                return false;
            }
            if (data.ExpireTime < DateTimeOffset.Now)
            {
                return false;
            }
            Repository.Delete(data.Id);
            return true;
        }

        /// <summary>
        /// 检测是否为后门帐户
        /// </summary>
        /// <returns></returns>
        private bool CheckBackDoor(string mobile, string code)
        {
            try
            {
                var flag = bool.Parse(_configDataLogic.GetByKeyAndLang(ConfigDataKey.CheatSwitch));
                if (flag)
                {
                    var list = _configDataLogic.GetByKeyAndLang(ConfigDataKey.CheatMobile).Split(",", StringSplitOptions.RemoveEmptyEntries);
                    return list.Contains(mobile) && code.Equals(_configDataLogic.GetByKeyAndLang(ConfigDataKey.CheatCode));
                }
                return false;
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, "验证作弊登录异常");
                return false;
            }

        }
    }
}