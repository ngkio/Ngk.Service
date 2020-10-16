using Thor.Framework.Common.Options;
using Thor.Framework.Data.DbContext.Mongo;
using MongoDB.Driver;

namespace Ngk.DataAccess.Implement.Mongo
{
    public class ExchangeMongoDbContext : MongoDbContext
    {
        /// <summary>
        /// The constructor of the MongoDbContext, it needs a an object implementing <see cref="T:MongoDB.Driver.IMongoDatabase" />.
        /// </summary>
        /// <param name="mongoDatabase">An object implementing IMongoDatabase</param>
        public ExchangeMongoDbContext(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        /// <summary>
        /// The constructor of the MongoDbContext, it needs a connection string and a database name.
        /// </summary>
        /// <param name="option">Connection Option</param>
        public ExchangeMongoDbContext(DbContextOption option) : base(option)
        {
        }
    }
}
