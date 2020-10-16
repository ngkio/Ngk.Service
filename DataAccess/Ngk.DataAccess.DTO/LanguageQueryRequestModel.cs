using System;
using Thor.Framework.Data.Model;
using Ngk.Common;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 查询语言表请求模型
    /// </summary>
    public class LanguageQueryRequestModel : LanguageBaseRequestModel
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public String StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public String EndTime { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数据
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 是否倒叙
        /// </summary>
        public bool IsSortOrderDesc { get; set; }


        public override void Verify()
        {
            base.Verify();
            if (String.IsNullOrEmpty(StartTime))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }

            if (!String.IsNullOrEmpty(EndTime))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }

            if (PageIndex == default(int))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }

            if (PageSize == default(int))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
            }
        }
    }
}
