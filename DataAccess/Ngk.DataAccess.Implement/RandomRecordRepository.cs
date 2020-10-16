using Thor.Framework.Data.DbContext.Mongo;
using MongoDB.Driver;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class RandomIdRepository : IRandomIdRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public RandomIdRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        /// <summary>
        /// 获取随机合约编号
        /// </summary>
        /// <returns>编号</returns>
        public long GetContractNum()
        {
            var filter = Builders<Counter>.Filter.Eq(p => p.Id, "randomid");
            var update = Builders<Counter>.Update.Inc(p => p.Value, 1);

            var options = new FindOneAndUpdateOptions<Counter>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };

            var result =  _mongoDbContext.GetCollection<Counter>().FindOneAndUpdateAsync(filter, update, options);
            result.Wait();
            return result.Result.Value;
        }
    }
}
