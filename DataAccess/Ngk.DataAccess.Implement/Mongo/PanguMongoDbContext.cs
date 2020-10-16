using Thor.Framework.Common.Options;
using Thor.Framework.Data.DbContext.Mongo;
using MongoDB.Driver;

namespace Ngk.DataAccess.Implement.Mongo
{
    public class PanguMongoDbContext : MongoDbContext
    {
        /// <summary>
        /// The constructor of the MongoDbContext, it needs a an object implementing <see cref="T:MongoDB.Driver.IMongoDatabase" />.
        /// </summary>
        /// <param name="mongoDatabase">An object implementing IMongoDatabase</param>
        public PanguMongoDbContext(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        /// <summary>
        /// The constructor of the MongoDbContext, it needs a connection string and a database name.
        /// </summary>
        /// <param name="option">Connection Option</param>
        public PanguMongoDbContext(DbContextOption option) : base(option)
        {
        }
    }
}
