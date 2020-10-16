using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class AccountAmountModel
    {
        [Required]
        public string Account { get; set; }

        public decimal Amount { get; set; }
    }


    public class AccountStakeModel
    {
        [Required]
        public string Account { get; set; }

        [Required]
        public string StakeAccount { get; set; }

        public bool IsTransfer { get; set; }

        public decimal Cpu { get; set; }

        public decimal Net { get; set; }
    }
}
