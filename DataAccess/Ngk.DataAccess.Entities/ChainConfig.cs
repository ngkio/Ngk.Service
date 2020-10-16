using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class ChainConfig
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(32)]
        public string ChainCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ChainId { get; set; }
        public Guid? Logo { get; set; }
        [Required]
        [StringLength(100)]
        public string DefaultNode { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsShow { get; set; }
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
