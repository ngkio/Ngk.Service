using System;
using Thor.Framework.Common.Pager;

namespace Ngk.Service.ManagerWebApi.Models
{
    /// <summary>
    /// 查询操作日志的参数
    /// </summary>
    public class SeachMonitorLogsModel: AdvQueryParam
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
        public String ExecuteStartTime { get; set; }

        /// <summary>
        /// 操作时间结束
        /// </summary>
        public String ExecuteEndTime { get; set; }

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
