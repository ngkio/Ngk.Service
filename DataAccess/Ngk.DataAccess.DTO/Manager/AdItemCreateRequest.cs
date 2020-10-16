using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public class AdItemCreateRequest
    {
        public Guid AdId { get; set; }

        public string AdCode { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public Guid? ImageId { get; set; }

        public string Url { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Price { get; set; }

        public int Order { get; set; }

        public bool IsDefault { get; set; }
    }
}
