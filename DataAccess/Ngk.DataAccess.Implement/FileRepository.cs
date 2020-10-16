using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.Interface.Mongo;
using File = Ngk.DataAccess.Entities.Mongo.File;

namespace Ngk.DataAccess.Implement
{
    public class FileRepository : MongoRepository<File>, IFileRepository
    {
        public FileRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {

        }
    }
}
