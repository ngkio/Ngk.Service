using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ngk.DataAccess.DTO
{
    public class TransferContractRepose
    {
        [JsonProperty("actions")]
        public List<TransferContractModel> Data { get; set; }
    }

    public class TransferContractModel
    {
        [JsonProperty("act")]
        public Act Act { get; set; }

        [JsonProperty("createdAt")]
        public DateTime Time { get; set; }

        [JsonProperty("trx_id")]
        public string TransferId { get; set; }

        [JsonProperty("block_num")]
        public long BlockNum { get; set; }
    }

    public class Act
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("authorization")]
        public List<Authorization> Authorization { get; set; }

        [JsonProperty("data")]
        public TransferData Data { get; set; }
    }

    public class TransferData
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        public string Amount
        {
            get
            {
                var list = Quantity.Split(' ');
                if (list.Length > 1)
                    return list[0];
                return "";
            }
        }

        public string Symbol
        {
            get
            {
                var list = Quantity.Split(' ');
                if (list.Length > 1)
                    return list[1];
                return "";
            }
        }

        [JsonProperty("memo")]
        public string Memo { get; set; }
    }

    public class Authorization
    {
        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("permission")]
        public string Permission { get; set; }
    }
}
