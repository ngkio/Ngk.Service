namespace Ngk.DataAccess.DTO
{
    public class AccountOrderInfo
    {
        public string Id { get; set; }

        public string ChainCode { get; set; }

        public decimal Price { get; set; }

        public decimal Rmb { get; set; }

        public int PayType { get; set; }

        public string Account { get; set; }

        public decimal Ram { get; set; }

        public decimal CpuStake { get; set; }

        public decimal NetStake { get; set; }

        public string ActiveKey { get; set; }

        public string OwnerKey { get; set; }

        public string PayImageUrl { get; set; }

        public string ContrtNum { get; set; }
    }
}
