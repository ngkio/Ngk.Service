using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class UserParam : AdvQueryParam
    {
        public string Mobile { get; set; }

        public int? Type { get; set; }
    }
}
