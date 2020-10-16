using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class TransferRecord
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [StringLength(36)]
        public string Mobile { get; set; }
        [StringLength(100)]
        public string TransferId { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long BlockNum { get; set; }
        [Required]
        [StringLength(12)]
        public string TokenCode { get; set; }
        [Required]
        [StringLength(32)]
        public string ChainCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Contract { get; set; }
        [Required]
        [StringLength(50)]
        public string FromAccount { get; set; }
        [Required]
        [StringLength(50)]
        public string ToAccount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransferTime { get; set; }
        [Column(TypeName = "decimal(21,4)")]
        public decimal TransferFee { get; set; }
        [Column(TypeName = "decimal(21,10)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "int(11)")]
        public int TransferState { get; set; }
        [Column(TypeName = "int(11)")]
        public int ErrorTimes { get; set; }
        [Required]
        [StringLength(1000)]
        public string Memo { get; set; }
        [Required]
        [StringLength(100)]
        public string Remark { get; set; }
        [StringLength(1024)]
        public string TransactionSign { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifyTime { get; set; }
    }
}
