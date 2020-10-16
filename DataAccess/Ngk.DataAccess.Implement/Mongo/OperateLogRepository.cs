using System;
using System.Linq.Expressions;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.DataAccess.Implement.Mongo
{
    public class OperateLogRepository : MongoRepository<OperateLog>, IOperateLogRepository
    {
        public OperateLogRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {

        }
        public override void Insert(OperateLog log)
        {
            try
            {
                base.Insert(log);
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex);
            }
        }


        /// <summary>
        /// 操作日志查询（分页）
        /// </summary>
        /// <returns></returns>
        public PagedResults<OperateLog> QueryOperateLogLogs(OperateLogParams modeLogParams)
        {
            Expression<Func<OperateLog, bool>> expression = p => true;

            if (modeLogParams.StartTime != default(DateTime))
            {
                expression = expression.And(p => p.CreateTime >= modeLogParams.StartTime);
            }

            if (modeLogParams.EndTime != default(DateTime))
            {
                if (modeLogParams.EndTime == modeLogParams.StartTime)
                {
                    modeLogParams.EndTime = modeLogParams.EndTime.AddDays(1).AddMilliseconds(-1);
                }
                expression = expression.And(p => p.CreateTime <= modeLogParams.EndTime);
            }

            if (!string.IsNullOrEmpty(modeLogParams.Function))
            {
                expression = expression.And(p => p.Function.Contains(modeLogParams.Function));
            }
            if (!string.IsNullOrEmpty(modeLogParams.IP))
            {
                expression = expression.And(p => p.ClientIp.Contains(modeLogParams.IP));
            }
            if (!string.IsNullOrEmpty(modeLogParams.Operate))
            {
                modeLogParams.Operate = modeLogParams.Operate.ToLower();
                expression = expression.And(p => p.Operate.ToLower().Contains(modeLogParams.Operate));
            }
            if (!string.IsNullOrEmpty(modeLogParams.ManagerAccount))
            {
                expression = expression.And(p => p.ManagerAccount.Contains(modeLogParams.ManagerAccount));
            }

            return QueryPagedResults(expression, modeLogParams);
        }
    }
}
