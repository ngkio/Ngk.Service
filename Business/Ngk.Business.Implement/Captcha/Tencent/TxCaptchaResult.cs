using Newtonsoft.Json;

namespace Ngk.Business.Implement.Captcha.Tencent
{
    public class TxCaptchaResult : CaptchaResult
    {
        private int _response;
        [JsonProperty("response")]
        public int Response
        {
            get => _response;
            set
            {
                _response = value;
                IsValid = _response == 1;
            }
        }

        private int _evelLevel;
        [JsonProperty("evil_level")]
        public int EvilLevel
        {
            get => _evelLevel;
            set
            {
                _evelLevel = value;
                Level = _evelLevel.ToString();
            }
        }

        private string _errMsg;
        [JsonProperty("err_msg")]
        public string ErrMsg
        {
            get => _errMsg;
            set
            {
                _errMsg = value;
                ErrorMessage = _errMsg;
            }
        }
    }
}
