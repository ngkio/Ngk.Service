using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Contacts
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(255)]
        public string Desc { get; set; }
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
