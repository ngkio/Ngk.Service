using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    /// <summary>
    /// 审核查询参数
    /// </summary>
    public class AuditParam : AdvQueryParam
    {
        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? AuditorId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int? AuditState { get; set; }
    }
}
