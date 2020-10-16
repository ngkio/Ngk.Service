using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class ChainModel
    {
        [Required]
        public string ChainCode { get; set; }
    }
}
