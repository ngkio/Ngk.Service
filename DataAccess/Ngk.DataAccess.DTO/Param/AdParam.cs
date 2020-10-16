using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class AdParam : AdvQueryParam
    {     
        public string Code { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }

        /// <summary>
        /// 页面
        /// </summary>
        public string Page { get; set; }
    }
}
