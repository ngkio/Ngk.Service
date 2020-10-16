using System;
using System.Collections.Generic;
using Thor.Framework.Data.DbContext.Mongo;
using MongoDB.Driver;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.Business.Implement.Mongo
{
    /// <summary>
    /// mongo日志文件
    /// </summary>
    public class MonitorLogsFactory
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MonitorLogsFactory(IMongoDbContext _mongoDbContext)
        {
            this._mongoDbContext = _mongoDbContext;
        }

        /// <summary>
        /// 获取今日的数据
        /// </summary>
        /// <returns></returns>
        public List<MonitorLog> GetTodayDataInfoList()
        {
            var filter = Builders<MonitorLog>.Filter.Gte<DateTime>(l => l.ExecuteStartTime, DateTime.Now.Date);
            return _mongoDbContext.GetCollection<MonitorLog>().Find(filter, null).ToList();
        }
    }
}