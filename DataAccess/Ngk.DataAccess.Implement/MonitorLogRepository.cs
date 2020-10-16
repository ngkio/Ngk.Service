using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Repository.Mongo;
using MongoDB.Driver;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.DataAccess.Implement
{

    public class MonitorLogRepository : MongoRepository<MonitorLog>, IMonitorLogRepository
    {
        private IMongoDbContext mongoDbContext;
        public MonitorLogRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }


        public List<MonitorLog> GetDateOInfo(FilterDefinition<MonitorLog> filter)
        {
            List<MonitorLog> monitorLogs = DbSet.Find(filter, null).ToList();
            return monitorLogs;
        }

        /// <summary>
        /// 获取短信记录(分页)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResults<MonitorLog> GetQuery(MonitorLogsParam param)
        {
            Expression<Func<MonitorLog, bool>> queryExp = p => true;
            if (!string.IsNullOrWhiteSpace(param.Path)) queryExp = queryExp.And(p => p.Path.Contains(param.Path));
            if (!string.IsNullOrWhiteSpace(param.IP)) queryExp = queryExp.And(p => p.IP.Contains(param.IP));
            if (param.ExecuteStartTime != default(DateTime))
            {
                queryExp = queryExp.And(p => p.ExecuteStartTime >= param.ExecuteStartTime);
            }
            if (param.ExecuteEndTime != default(DateTime))
            {
                if (param.ExecuteEndTime == param.ExecuteStartTime)
                {
                    param.ExecuteEndTime = param.ExecuteEndTime.AddDays(1).AddMilliseconds(-1);
                }
                queryExp = queryExp.And(p => p.ExecuteStartTime <= param.ExecuteEndTime);
            }

            //输入参数
            if (!string.IsNullOrEmpty(param.RequestParams))
            {
                queryExp = queryExp.And(p => p.ActionParams.Contains(param.RequestParams));
            }
            //输出参数
            if (!string.IsNullOrEmpty(param.ResponseParams))
            {
                queryExp = queryExp.And(p => p.Response.Contains(param.ResponseParams));
            }

            //输出Code
            if (!string.IsNullOrEmpty(param.ResponseCode))
            {
                //Regex reg = new Regex(".*code[:\"'\\s]+([^\"']*)[\"'].*");
                //queryExp = queryExp.And(p => reg.Replace(p.Response, "$1")== param.ResponseCode);
                string code = string.Format("\"code\":\"{0}\"", param.ResponseCode);
                queryExp = queryExp.And(p => p.Response.Contains(code));
            }

            if (string.IsNullOrEmpty(param.SortName))
            {
                param.SortName = "ExecuteStartTime";
            }

            param.SortList = new Dictionary<string, bool> {

                    {param.SortName,param.IsSortOrderDesc}
            };

            return QueryPagedResults(queryExp, param);
        }


        /// <summary>
        /// 获取今日的数据
        /// </summary>
        /// <returns></returns>
        public List<MonitorLog> GetTodayDataInfoList()
        {
            var date = DateTime.Now.Date;
            var filter = Builders<MonitorLog>.Filter.Gte<DateTime>(l => l.ExecuteStartTime, date);
            return DbSet.Find(filter, null).ToList();
        }
    }
}
