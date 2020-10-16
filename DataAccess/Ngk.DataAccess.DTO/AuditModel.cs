using System;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 审核操作模型
    /// </summary>
    public class AuditModel
    {
        public Guid Id { get; set; }

        public bool IsAllowPublishToken { get; set; }

        public bool IsNew { get; set; }

        public string Remark { get; set; }
    }
}
