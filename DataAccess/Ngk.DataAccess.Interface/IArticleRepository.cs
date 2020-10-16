using Ngk.DataAccess.Entities;
using Thor.Framework.Repository.Relational;

namespace Ngk.DataAccess.Interface
{
    public interface IArticleRepository : IRepository<Article>
    {
        Article GetByCode(string code);
    }
}

