using System;
using System.Collections.Generic;
using System.Text;

namespace Ngk.DataAccess.DTO
{
    public class VersionModel
    {
        /// <summary>
        /// 版本名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 介绍说明
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 是否强制升级
        /// </summary>
        public bool IsForce { get; set; }

        /// <summary>
        /// 下载更新地址
        /// </summary>
        public string DownUrl { get; set; }
    }
}
