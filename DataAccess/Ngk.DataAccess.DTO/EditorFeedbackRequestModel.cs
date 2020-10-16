using System;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 编辑用户反馈参数 
    /// </summary>
    public class EditorFeedbackRequestModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id{ get; set; }

        /// <summary>
        /// 业务状态 1：初始化，2：已编辑，3：已确认
        /// </summary>
        public int BusinessState { get; set; }

        /// <summary>
        ///  反馈分类
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
    }
}
