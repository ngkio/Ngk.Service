using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Thor.Framework.Data.Model.Mongo;

namespace Ngk.DataAccess.Entities.Mongo
{
    public class FileHeader : IMongoEntityModel
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string FileExt { get; set; }
        public string ContentType { get; set; }
        public long Length { get; set; }
        public int Category { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public int State { get; set; }

        /// <summary>兼容新字段</summary>
        [BsonExtraElements]
        public BsonDocument NewElements { get; set; }
    }
}
