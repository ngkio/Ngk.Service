using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Thor.Framework.Data.Model.Mongo;

namespace Ngk.DataAccess.Entities.Mongo
{
    public class File : IMongoEntityModel
    {
        public Guid Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        /// <summary>兼容新字段</summary>
        [BsonExtraElements]
        public BsonDocument NewElements { get; set; }
    }
}
