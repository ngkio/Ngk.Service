using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Invite
    {
        public Guid Id { get; set; }
        public Guid InviteUserId { get; set; }
        [Required]
        [StringLength(8)]
        public string InviteCode { get; set; }
        [Required]
        [StringLength(15)]
        public string InviteMobile { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [StringLength(15)]
        public string UserMobile { get; set; }
        [Required]
        [StringLength(8)]
        public string UserInviteCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
