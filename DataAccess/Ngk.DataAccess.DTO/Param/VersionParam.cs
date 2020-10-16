using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{

    public class VersionParam : AdvQueryParam
    {
        public int? Number { get; set; }

        public string Name { get; set; }

        public int? ClientType { get; set; }
    }
}
