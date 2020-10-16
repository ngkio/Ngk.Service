using System;
using System.Collections.Generic;
using System.Text;
using Thor.Framework.Data.Model;
using Ngk.Common;

namespace Ngk.DataAccess.Entities
{
    public class NoticeRequestModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public String Content { get; set; }


        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 改公告是否涉及关系系统
        /// 1：涉及 0：不涉及
        /// </summary>
        public byte IsShutdownSystem { get; set; }

        /// <summary>
        /// 是否显示一次
        /// 1：涉及 0：不涉及
        /// </summary>
        public byte IsOnlyOne { get; set; }


        public void InsertVerify()
        {
            if (string.IsNullOrEmpty(Content))
                throw new BusinessException(BusinessResultCode.ArgumentError, " 参数错误或无效");

            if (string.IsNullOrEmpty(ServiceName))
            {
                ServiceName = "Ngk";
            }


            if (string.IsNullOrEmpty(Title))
                throw new BusinessException(BusinessResultCode.ArgumentError, " 参数错误或无效");
        }
    }
}
