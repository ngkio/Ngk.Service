using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Language
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Code { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Required]
        [StringLength(64)]
        public string Desc { get; set; }
        [Required]
        [StringLength(255)]
        public string En { get; set; }
        [Required]
        [StringLength(255)]
        public string Zh { get; set; }
        [Required]
        [StringLength(255)]
        public string Ko { get; set; }
        [Required]
        [StringLength(255)]
        public string Fr { get; set; }
        [Required]
        [StringLength(255)]
        public string De { get; set; }
        [Required]
        [StringLength(255)]
        public string Es { get; set; }
        [Required]
        [StringLength(255)]
        public string Ru { get; set; }
        [Required]
        [StringLength(255)]
        public string Pt { get; set; }
        [Required]
        [StringLength(255)]
        public string Ar { get; set; }
        [Required]
        [StringLength(255)]
        public string Tw { get; set; }
        [Required]
        [StringLength(255)]
        public string Ja { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "int(11)")]
        public int State { get; set; }
        [StringLength(50)]
        public string Deleter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeleteTime { get; set; }
    }
}
