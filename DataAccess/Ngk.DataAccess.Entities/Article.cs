using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngk.DataAccess.Entities
{
    public partial class Article
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(16)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "mediumtext")]
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "int(11)")]
        public int State { get; set; }
    }
}
