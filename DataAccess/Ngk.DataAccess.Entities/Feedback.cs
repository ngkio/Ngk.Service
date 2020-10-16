using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Feedback
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(15)]
        public string Link { get; set; }
        [Required]
        [StringLength(1000)]
        public string Connect { get; set; }
        [Column(TypeName = "int(8)")]
        public int Type { get; set; }
        [Column(TypeName = "int(8)")]
        public int BusinessState { get; set; }
        [StringLength(256)]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(50)]
        public string Deleter { get; set; }
        [StringLength(512)]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DeleteTime { get; set; }
        [Required]
        [StringLength(50)]
        public string DeleteIp { get; set; }
        [Column(TypeName = "int(11)")]
        public int State { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
