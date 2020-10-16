using Thor.Framework.Data.Model.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ngk.DataAccess.Entities.Mongo
{

    public class SmsSend : IMongoEntityModel
    {
        public Guid Id { get; set; }
        public string Mobile { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public string SmsProxy { get; set; }
        public string SmsProxyResp { get; set; }
        public bool IsSuccess { get; set; }
        public int Client { get; set; }
        public string LoginIp { get; set; }

        /// <summary>兼容新字段</summary>
        [BsonExtraElements]
        public BsonDocument NewElements { get; set; }
    }
}
