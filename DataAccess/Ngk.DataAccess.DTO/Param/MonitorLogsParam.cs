using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitorLogsParam: AdvQueryParam
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        public String UserMobile { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public String Path { get; set; }

        /// <summary>
        /// 操作时间开始
        /// </summary>
        public DateTime ExecuteStartTime { get; set; }

        /// <summary>
        /// 操作时间结束
        /// </summary>
        public DateTime ExecuteEndTime { get; set; }

        /// <summary>
        /// 请求的IP
        /// </summary>
        public String IP { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public String RequestParams { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public String ResponseParams { get; set; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public String ResponseCode { get; set; }
    }
}
