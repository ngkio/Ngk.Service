using System;

namespace Ngk.DataAccess.DTO.Manager
{
   public  class VersionEditRequest : VersionCreateRequest
    {
        public Guid Id { get; set; }
    }
}
