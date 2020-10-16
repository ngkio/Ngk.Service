using System;
using System.Collections.Generic;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class UserFavoriteParam : AdvQueryParam
    {
        public Guid? UserId { get; set; }

        public string Mobile { get; set; }

        public int? ItemType { get; set; }

        public string ChainCode { get; set; }

        public List<Guid> RelatedIds { get; set; }
    }
}
