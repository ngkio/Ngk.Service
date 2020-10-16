using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class User
    {
        public Guid Id { get; set; }
        [StringLength(30)]
        public string ParentUserMobile { get; set; }
        [StringLength(30)]
        public string ParentUserCode { get; set; }
        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Column(TypeName = "int(11)")]
        public int UserType { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public Guid? AvatarId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nickname { get; set; }
        [Required]
        [StringLength(10)]
        public string InviteCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LoginTime { get; set; }
        [Required]
        [StringLength(30)]
        public string LoginIp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifyTime { get; set; }
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
