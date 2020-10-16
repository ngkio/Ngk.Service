using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class SearchRecordParam : AdvQueryParam
    {
        public string Mobile { get; set; }

        public string ChainCode { get; set; }

        public int? Type { get; set; }

        public Guid? UserId { get; set; }
    }
}
