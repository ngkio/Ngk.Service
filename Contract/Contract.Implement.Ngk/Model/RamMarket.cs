using System;
using Newtonsoft.Json;

namespace Contract.Implement.Ngk.Model
{
    public class RamMarket
    {
        [JsonProperty("base")]
        public RamWeight Base { get; set; }

        [JsonProperty("quote")]
        public RamWeight Quote { get; set; }
    }

    public class RamWeight
    {
        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        /// <summary>
        /// 获取余额数字
        /// </summary>
        /// <returns></returns>
        public decimal GetBalanceValue()
        {
            var str = Balance.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (str.Length == 2)
            {
                decimal.TryParse(str[0], out var balance);
                return balance;
            }
            return 0;
        }
    }
}
