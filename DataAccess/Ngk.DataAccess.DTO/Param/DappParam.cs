using System;
using System.Collections.Generic;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class DappParam : AdvQueryParam
    {
        public Guid? DappId { get; set; }

        public List<Guid> DappIds { get; set; }

        public string Mobile { get; set; }

        public string ChainCode { get; set; }

        public string Name { get; set; }

        public string NameOrUrl { get; set; }

        public string Tag { get; set; }

        public bool OnlySearchNew { get; set; }

        public bool? IsSystem { get; set; }

        /// <summary>
        /// 是否返回收藏属性,为true时手机号必传
        /// </summary>
        public bool IsReturnFavorite { get; set; }

        /// <summary>
        /// 排除的DappId
        /// </summary>
        public List<String> ExcludeDappid { get; set; }

        /// <summary>
        /// 找到DappId为null的数据,或者是存在于DappExternalIds的参数
        /// </summary>
        public bool IsNullOrExistExternalId { get; set; }

        /// <summary>
        /// 根据DappId获取数据
        /// </summary>
        public List<String> DappExternalIds { get; set; }
    }
}
