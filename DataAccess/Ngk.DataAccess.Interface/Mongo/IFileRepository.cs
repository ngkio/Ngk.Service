using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.Interface.Mongo
{
    public interface IFileRepository : IMongoRepository<File>
    {
    }
}
