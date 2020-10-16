using System;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface ILoginLogRepository : IRepository<LoginLog>
    {
        /// <summary>
        /// 获取用户最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        LoginLog GetAccountLastLog(Guid userId);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResults<LoginLog> GetQuery(LoginLogParam model);
    }
}
