using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public class TokenCreateModel
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 符号
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// LOGO
        /// </summary>
        public Guid? Logo { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 是否为主流Token
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 是否需要审核
        /// </summary>
        public bool IsNeedAudit { get; set; }

        public int Order { get; set; }

        public string Contract { get; set; }
    }
}
