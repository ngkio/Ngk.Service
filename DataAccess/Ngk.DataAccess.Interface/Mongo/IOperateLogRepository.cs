using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.Interface.Mongo
{
    public interface IOperateLogRepository : IMongoRepository<OperateLog>
    {
        /// <summary>
        /// 分页查询操作日志
        /// </summary>
        /// <param name="modeLogParams"></param>
        /// <returns></returns>
        PagedResults<OperateLog> QueryOperateLogLogs(OperateLogParams modeLogParams);
    }
}
