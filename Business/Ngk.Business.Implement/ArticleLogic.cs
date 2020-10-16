using Thor.Framework.Business.Relational;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class ArticleLogic : BaseBusinessLogic<Article, IArticleRepository>, IArticleLogic
    {
        #region ctor
        public ArticleLogic(IArticleRepository repository) : base(repository)
        {

        }

        public Article GetByCode(string code)
        {
            return Repository.GetByCode(code);
        }
        #endregion

    }
}


