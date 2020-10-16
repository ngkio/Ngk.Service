using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public  class ConfigDataEditRequest : ConfigDataCreateRequest
    {
        public Guid Id { get; set; }
    }
}
