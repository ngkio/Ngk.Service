using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class ConfigData
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ConfigKey { get; set; }
        [Required]
        [StringLength(1024)]
        public string ConfigValue { get; set; }
        [Required]
        [StringLength(255)]
        public string Remark { get; set; }
        [Required]
        [Column(TypeName = "bit(1)")]
        public bool IsApp { get; set; }
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
