using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Manager
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Account { get; set; }
        [Required]
        [StringLength(255)]
        public string Pwd { get; set; }
        [Required]
        [StringLength(16)]
        public string Salt { get; set; }
        [Column(TypeName = "int(11)")]
        public int ErrorTimes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastErrorTime { get; set; }
        [Column(TypeName = "int(50)")]
        public int ManagerType { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginTime { get; set; }
        [Column(TypeName = "int(11)")]
        public int BusinessState { get; set; }
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
