using System;
using System.Collections.Generic;
using System.Text;
using Thor.Framework.Data.Model.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ngk.DataAccess.Entities.Mongo
{
    /// <summary>
    /// 存在mongo中的公告信息
    /// </summary>
    public class Notice : IMongoEntityModel
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

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
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 改公告是否涉及关系系统
        /// 1：涉及 0：不涉及
        /// </summary>
        public byte IsShutdownSystem { get; set; }

        /// <summary>
        /// 是否显示一次
        /// </summary>
        public byte IsOnlyOne { get; set; }

        /// <summary>
        /// 冗余映射其它字段
        /// </summary>
        [BsonExtraElements]
        public BsonDocument NewElements { get; set; }

        /// <summary>
        /// 创建时间字符串
        /// </summary>
        public string CreateTimeStr
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}
