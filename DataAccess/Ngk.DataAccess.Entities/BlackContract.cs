using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class BlackContract
    {
        public Guid Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long ContractId { get; set; }
        [Required]
        [StringLength(15)]
        public string Initiator { get; set; }
        [Required]
        [StringLength(15)]
        public string Target { get; set; }
        [Required]
        [StringLength(200)]
        public string Desc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpireTime { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long ExpireTimestamp { get; set; }
        [Required]
        [StringLength(100)]
        public string TransferId { get; set; }
        [Column(TypeName = "int(11)")]
        public int ProposalState { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "int(11)")]
        public int State { get; set; }
    }
}
