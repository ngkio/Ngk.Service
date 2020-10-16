using System.Collections.Generic;
using System.Linq;

namespace Ngk.DataAccess.DTO
{
    public class TokenBalance
    {
        public decimal TotalBalanceByDoller
        {
            get { return Items.Sum(p => p.BalanceByDoller); }
        }

        public decimal TotalBalanceByRmb
        {
            get { return Items.Sum(p => p.BalanceByRmb); }
        }

        public List<TokenBalanceItem> Items { get; set; }

    }

    public class TokenBalanceItem
    {
        public string Name { get; set; }

        public string Logo { get; set; }

        public string Issuer { get; set; }

        public decimal Balance { get; set; }

        public decimal BalanceByDoller { get; set; }

        public decimal BalanceByRmb { get; set; }

        public decimal StakeAmount { get; set; }
    }
}
