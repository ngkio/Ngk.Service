using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.DataAccess.Implement.Mongo
{
    public class FileHeaderRepository : MongoRepository<FileHeader>, IFileHeaderRepository
    {
        public FileHeaderRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
