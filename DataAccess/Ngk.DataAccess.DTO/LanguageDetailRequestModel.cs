namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 语言详情实体
    /// 可用于编辑与新增
    /// </summary>
    public class LanguageDetailRequestModel : LanguageBaseRequestModel
    {
        /// <summary>
        /// 英文
        /// </summary>
        public string En { get; set; }

        /// <summary>
        /// 韩文
        /// </summary>
        public string Ko { get; set; }
    }
}
