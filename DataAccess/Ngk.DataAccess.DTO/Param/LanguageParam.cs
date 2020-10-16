using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    public class LanguageParam : AdvQueryParam
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }
}
