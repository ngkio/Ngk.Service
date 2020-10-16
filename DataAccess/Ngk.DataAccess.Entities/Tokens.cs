using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Tokens
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Symbol { get; set; }
        [Required]
        [StringLength(50)]
        public string ChainCode { get; set; }
        [Column(TypeName = "int(11)")]
        public int Order { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsSystem { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsMain { get; set; }
        [Required]
        [StringLength(100)]
        public string Issuer { get; set; }
        [Required]
        [StringLength(100)]
        public string Contract { get; set; }
        public Guid? Logo { get; set; }
        [Column(TypeName = "int(11)")]
        public int Precision { get; set; }
        [Column(TypeName = "int(11)")]
        public int TransactionPrecision { get; set; }
        [Required]
        [StringLength(255)]
        public string WebSite { get; set; }
        [Required]
        [StringLength(255)]
        public string WhitePaperUrl { get; set; }
        [Required]
        [StringLength(255)]
        public string FaceBookUrl { get; set; }
        [Required]
        [StringLength(255)]
        public string TwitterUrl { get; set; }
        [Required]
        [StringLength(50)]
        public string IssueState { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IssueDate { get; set; }
        [Required]
        [StringLength(100)]
        public string IssueCost { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal RmbPrice { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal DollarPrice { get; set; }
        [Required]
        [StringLength(255)]
        public string Desc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "int(11)")]
        public int State { get; set; }
        [StringLength(50)]
        public string Deleter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeleteTime { get; set; }
        [StringLength(50)]
        public string DeleteIp { get; set; }
    }
}
