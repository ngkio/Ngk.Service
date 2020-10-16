using System.Collections.Generic;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Mongo;
using MongoDB.Driver;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.Interface.Mongo
{
    public interface IMonitorLogRepository : IMongoRepository<MonitorLog>
    {
        /// <summary>
        /// 根据赛选条件获取数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<MonitorLog> GetDateOInfo(FilterDefinition<MonitorLog> filter);


        /// <summary>
        /// 根据赛选条件获取分页数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResults<MonitorLog> GetQuery(MonitorLogsParam param);


        /// <summary>
        /// 根据筛选条件获取操作日志
        /// </summary>
        /// <returns></returns>
        List<MonitorLog> GetTodayDataInfoList();
    }
}
