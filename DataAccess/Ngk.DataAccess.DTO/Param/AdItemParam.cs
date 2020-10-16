using System;
using System.Collections.Generic;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class AdItemParam : AdvQueryParam
    {     
        public Guid? AdId { get; set; }

        public List<Guid> AdIds { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public bool IsDefault { get; set; }
         public string IsHistory { get; set; }
        
    }
}
