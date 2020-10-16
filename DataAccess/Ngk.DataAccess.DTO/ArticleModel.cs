using System;
using System.Collections.Generic;
using System.Text;

namespace Ngk.DataAccess.DTO
{
    public class ArticleModel
    {
        /// <summary>
        /// 文章编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
