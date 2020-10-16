using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO
{
    public class GetUserFeedbackRequestModel: AdvQueryParam
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// 0、未分类1、程序崩溃2、帐号异常3、功能异常4、优化建议
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 业务状态
        /// </summary>
        public int BusinessState { get; set; }

        /// <summary>
        /// 创建开始时间
        /// </summary>
        public String StartCreateTime { get; set; }

        /// <summary>
        /// 创建结束时间
        /// </summary>
        public String EndCreateTime { get; set; }
    }
}
