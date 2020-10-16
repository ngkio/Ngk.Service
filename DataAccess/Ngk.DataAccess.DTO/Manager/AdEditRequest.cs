using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public class AdEditRequest : AdCreateRequest
    {
        public Guid Id { get; set; }
    }
}
