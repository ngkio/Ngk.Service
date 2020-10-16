using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class AccountParam : AdvQueryParam
    {
        public int? Type { get; set; }

        public string Mobile { get; set; }
        public string Account { get; set; }
    }
}
