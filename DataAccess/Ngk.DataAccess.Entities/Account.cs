using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Account
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ChainCode { get; set; }
        [Required]
        [Column("Account")]
        [StringLength(50)]
        public string Account1 { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Required]
        [StringLength(256)]
        public string PublicKey { get; set; }
        [Required]
        [StringLength(50)]
        public string Access { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsDefault { get; set; }
        [Required]
        [Column(TypeName = "bit(1)")]
        public bool IsCheck { get; set; }
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
