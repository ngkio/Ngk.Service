using Newtonsoft.Json;

namespace Contract.Interface.Model
{
    public class PayRecordModel
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("order_random")]
        public uint OrderRandom { get; set; }
        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("primary_pay_quantity")]
        public string PrimaryPayQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("secondary_pay_quantity")]
        public string SecondaryPayQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("thirdary_pay_quantity")]
        public string ThirdaryPayQuantity { get; set; }
        /// <summary>
        /// 1 普通  2 支付转账
        /// </summary>
        [JsonProperty("pay_mode")]
        public int PayMode { get; set; }
        [JsonProperty("pay_account")]
        public string PayAccount { get; set; }
        [JsonProperty("transfer_to_account")]
        public string TransferToAccount { get; set; }
        [JsonProperty("pay_time")]
        public long PayTime { get; set; }

    }
}
