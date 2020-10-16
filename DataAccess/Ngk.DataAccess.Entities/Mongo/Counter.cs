using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Ngk.DataAccess.Entities.Mongo
{
    public class Counter
    {
        [BsonId]
        public string Id { get; set; }

        public long Value { get; set; }

    }
}
