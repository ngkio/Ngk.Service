using Thor.Framework.Data.Model;
using Ngk.Common;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 语言详情
    /// </summary>
    public class LanguageBaseRequestModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Guid { get; set; }

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
        /// 中文提示
        /// </summary>
        public string Zh { get; set; }

        /// <summary>
        /// 参数验证
        /// </summary>
        public virtual void Verify()
        {
            if (string.IsNullOrEmpty(Code))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }

            if (string.IsNullOrEmpty(Desc))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }

            if (Type == default(int))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }

            if (string.IsNullOrEmpty(Zh))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }
        }
    }
}
