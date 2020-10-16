using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Interface.Model
{
    public class TransferResult
    {
        public string TransactionId { get; set; }

        public decimal GasPrice { get; set; }

        public long Nonce { get; set; }
    }
}
