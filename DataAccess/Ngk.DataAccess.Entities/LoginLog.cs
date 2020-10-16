using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class LoginLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [StringLength(15)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(250)]
        public string Uuid { get; set; }
        [Required]
        [StringLength(30)]
        public string LoginIp { get; set; }
        [Required]
        [StringLength(250)]
        public string LoginClient { get; set; }
        [Column(TypeName = "int(11)")]
        public int LoginMethod { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
