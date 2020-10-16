using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Node
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ChainCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string HttpAddress { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsSuper { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [Column(TypeName = "int(11)")]
        public int ErrorCount { get; set; }
        [Column(TypeName = "int(11)")]
        public int TimeOut { get; set; }
        [Column(TypeName = "int(11)")]
        public int Priority { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool QueryAlternative { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool PlayerAlternative { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool ServerAlternative { get; set; }
        [Required]
        [StringLength(50)]
        public string Deleter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeleteTime { get; set; }
        [Required]
        [StringLength(50)]
        public string DeleteIp { get; set; }
        [Column(TypeName = "int(11)")]
        public int State { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
