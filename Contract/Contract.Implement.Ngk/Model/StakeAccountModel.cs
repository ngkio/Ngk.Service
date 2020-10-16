using Newtonsoft.Json;

namespace Contract.Implement.Ngk.Model
{
    public class StakeAccountModel
    {
        [JsonProperty("locked_balance")]
        public string LockedBalance { get; set; }
    }
}
