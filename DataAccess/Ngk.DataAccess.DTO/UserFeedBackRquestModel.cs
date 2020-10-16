using System;
using System.Collections.Generic;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 用户反馈请求实体
    /// </summary>
    public class UserFeedBackRquestModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public String Mobile { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Connect { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        public List<String> fileids  { get; set; }
    }
}
