using System;
using System.Linq.Expressions;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Mongo;
using MongoDB.Driver;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.DataAccess.Implement.Mongo
{
    public class NoticeRepository: MongoRepository<Notice>, INoticeRepository
    {
        //private IMongoDbContext _mongoDdContext;
        private readonly PanguMongoDbContext _mongoDbContext;
        public NoticeRepository(PanguMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        /// <summary>
        /// 根据时间获取
        /// </summary>
        /// <param name="time"></param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public Notice GetNextInfoByTime(DateTime time, string serviceName)
        {
            Notice result = null;
            var filterDatetime = Builders<Notice>.Filter.Gt<DateTime>(l => l.CreateTime, time);
            var filterServiceName = Builders<Notice>.Filter.Regex(p => p.ServiceName, string.Format("/^{0}$/i", serviceName));

            var filterBuilders = Builders<Notice>.Filter;
            var sort = Builders<Notice>.Sort.Descending(p => p.CreateTime);
            var filter = filterBuilders.And(filterDatetime, filterServiceName);

            var listInfo = DbSet.Find(filter).Sort(sort).Limit(1);
            if (listInfo != null && listInfo.Any())
            {
                result = listInfo.FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取公告（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedResults<Notice> GetQuery(NoticeParams model)
        {
            Expression<Func<Notice, bool>> queryExp = p => true;

            if (!string.IsNullOrEmpty(model.Content))
            {
                queryExp = queryExp.And(p => p.Content.Contains(model.Content));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                queryExp = queryExp.And(p => p.Title.Contains(model.Title));
            }
            if (!string.IsNullOrEmpty(model.ServiceName))
            {
                queryExp = queryExp.And(p => p.ServiceName == model.ServiceName);
            }
            if (model.IsShutdownSystem != 2)
            {
                queryExp = queryExp.And(p => p.IsShutdownSystem == model.IsShutdownSystem);
            }
            return QueryPagedResults(queryExp, model);
        }
    }
}
