using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class NewItemParam : AdvQueryParam
    {
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public String Tag { get; set; }
    }
}
