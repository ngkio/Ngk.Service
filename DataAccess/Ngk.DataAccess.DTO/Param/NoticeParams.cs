using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class NoticeParams : AdvQueryParam
    {
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 改公告是否涉及关系系统
        /// 1：涉及 0：不涉及
        /// </summary>
        public byte IsShutdownSystem { get; set; }
    }
}
