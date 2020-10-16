using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{

    public class TokenMarketInfosParams: AdvQueryParam
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 合约
        /// </summary>
        public string Contract { get; set; }
    }
}
