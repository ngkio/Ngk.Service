using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Thor.Framework.Data.Model.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace Ngk.DataAccess.Entities.Mongo
{
    public class OperateLog : IMongoEntityModel
    {

        public Guid Id { get; set; }
        /// <summary>
        /// 管理员Id
        /// </summary>
        public Guid ManagerId { get; set; }
        /// <summary>
        /// 后台帐号
        /// </summary>
        public string ManagerAccount { get; set; }
        /// <summary>
        /// 操作方式
        /// </summary>
        public string Operate { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// 原始值
        /// </summary>
        public string OriginalValue { get; set; }
        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>兼容新字段</summary>
        [BsonExtraElements]
        public BsonDocument NewElements { get; set; }
    }
}
