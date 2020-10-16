using System;
using System.Collections.Generic;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class TokenParam : AdvQueryParam
    {
        public string ChainCode { get; set; }

        public string Name { get; set; }

        public string Issuer { get; set; }

        /// <summary>
        /// 是否是主流
        /// </summary>
        public bool? IsMain { get; set; }

        /// <summary>
        /// Mongo中已经存在的数据key
        /// </summary>
        public List<String> Keys { get; set; }

        /// <summary>
        /// IsExist  判断是否存在
        /// </summary>
        public bool? IsExist { get; set; }
    }
}
