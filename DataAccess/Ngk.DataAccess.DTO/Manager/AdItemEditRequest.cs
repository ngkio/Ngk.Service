using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public class AdItemEditRequest : AdItemCreateRequest
    {
        public Guid Id { get; set; }
    }
}
