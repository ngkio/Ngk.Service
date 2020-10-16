using System;
using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class AddTransferRecordRequest
    {
        [Required]
        public string ChainCode { get; set; }

        [Required]
        public string TransferId { get; set; }

        [Required]
        public string TokenCode { get; set; }

        [Required]
        public string Contract { get; set; }

        [Required]
        public string FromAccount { get; set; }

        [Required]
        public string ToAccount { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// 区块号
        /// </summary>
        public long BlockNum { get; set; }

        public DateTime? TransferTime { get; set; }

        public string Memo { get; set; }
    }
}
