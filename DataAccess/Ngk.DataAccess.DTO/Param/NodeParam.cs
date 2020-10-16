using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class NodeParam : AdvQueryParam
    {
        public string Name { get; set; }

        public string ChainCode { get; set; }

        public string HttpAddress { get; set; }

        public string Address { get; set; }

        public bool? IsSuper { get; set; }

        public bool? QueryAlternative { get; set; }

        public bool? PlayerAlternative { get; set; }

        public bool? ServerAlternative { get; set; }
    }
}
