using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Captcha
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(15)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(8)]
        public string Code { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpireTime { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsVerify { get; set; }
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
