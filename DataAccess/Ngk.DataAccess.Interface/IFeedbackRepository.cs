using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="model"></param>
        void Insert(Feedback model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResults<Feedback> GetQuery(FeedBackParams param);
    }
}

