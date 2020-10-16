using System;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 添加权限变更日志
    /// </summary>
    public class JurisdictionChangerModel
    {
        /// <summary>
        /// 链体系CODE
        /// </summary>
        public string ChainCode { get; set; }

        /// <summary>
        /// 钱包账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 旧数据
        /// </summary>
        public String OldKey { get; set; }

        /// <summary>
        /// 新数据
        /// </summary>
        public String NewKey { get; set; }

        /// <summary>
        /// 公钥类型
        /// </summary>
        public String KeyType { get; set; }
    }
}
