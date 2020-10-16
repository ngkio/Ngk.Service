using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Version
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Number { get; set; }
        [Column(TypeName = "int(255)")]
        public int ClientType { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsMustUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(255)]
        public string Desc { get; set; }
        [Required]
        [StringLength(1000)]
        public string Connect { get; set; }
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
