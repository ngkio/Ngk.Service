using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class CreateAccountRecord
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Uuid { get; set; }
        [Required]
        [StringLength(30)]
        public string ClientIp { get; set; }
        [Required]
        [StringLength(50)]
        public string Owner { get; set; }
        [Required]
        [StringLength(32)]
        public string ChainCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Account { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Required]
        [StringLength(100)]
        public string PublicKey { get; set; }
        [Required]
        [StringLength(50)]
        public string Access { get; set; }
        [Required]
        [StringLength(255)]
        public string Remark { get; set; }
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
