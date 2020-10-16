using System;
using System.Collections.Generic;
using System.Text;
using Thor.Framework.Common.Pager;
using Ngk.DataAccess.Entities;

namespace Pangu.ServiceCenter.DataAccess.Entities
{
    public class QueryNoticeRequestModel : NoticeRequestModel
    {
        /// <summary>
        ///  每一页数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        //public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        //public DateTime EndTime { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        //public string SortName { get; set; }

        /// <summary>
        /// 是否倒序
        /// </summary>
        //public bool IsSortOrderDesc { get; set; }
    }
}
