using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.Interface.Mongo
{
    public interface IFileHeaderRepository : IMongoRepository<FileHeader>
    {
    }
}
