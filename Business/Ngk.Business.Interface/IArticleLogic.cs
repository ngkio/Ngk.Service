using System.Threading.Tasks;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IArticleLogic : IBusinessLogic<Article>
    {
        Article GetByCode(string code);
    }
}

