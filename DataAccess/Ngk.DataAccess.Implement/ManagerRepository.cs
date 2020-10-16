using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}


