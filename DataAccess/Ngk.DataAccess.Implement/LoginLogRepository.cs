using System;
using System.Linq;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class LoginLogRepository : BaseRepository<LoginLog>, ILoginLogRepository
    {
        public LoginLogRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public override int Add(LoginLog entity, bool withTrigger = false)
        {
            if (entity.LoginClient.Length > 250)
            {
                entity.LoginClient = entity.LoginClient.Substring(0, 250);
            }
            entity.CreateTime = DateTime.UtcNow;
            return base.Add(entity, withTrigger);
        }

        /// <summary>
        /// 获取用户最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public LoginLog GetAccountLastLog(Guid userId)
        {
            var log = DbSet.Where(p => p.UserId == userId).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return log;
        }


        /// <summary>
        /// 重写高级查询
        /// </summary>
        /// <typeparam name="TQueryParam"></typeparam>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        protected override IQueryable<LoginLog> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam);

            if (queryParam is LoginLogParam)
            {
                var param = queryParam as LoginLogParam;
                if (!string.IsNullOrEmpty(param.Mobile))
                {
                    result = result.Where(p => p.Mobile.Contains(param.Mobile));
                }
                if (!string.IsNullOrEmpty(param.IP))
                {
                    result = result.Where(p => p.LoginIp.Contains(param.IP));
                }
                if (param.StartTime != default(DateTime))
                {
                    result = result.Where(p => p.CreateTime >= param.StartTime);
                }

                if (param.EndTime != default(DateTime))
                {
                    if (param.EndTime != DateTime.MaxValue)
                        param.EndTime = param.EndTime.AddDays(1).AddMilliseconds(-1);
                    result = result.Where(p => p.CreateTime <= param.EndTime);
                }
            }
            return result;
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedResults<LoginLog> GetQuery(LoginLogParam model)
        {
            var iQueryable = GetAdvQuery(model);
            return iQueryable.ToPagedResults<LoginLog, LoginLog>(model);
        }
    }
}
