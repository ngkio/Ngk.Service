using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public override int Add(Account entity, bool withTrigger = false)
        {
            entity.CreateTime = DateTime.UtcNow;
            return base.Add(entity, withTrigger);
        }

        protected override IQueryable<Account> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam);
            result = result.Where(p => p.State == (int)EnumState.Normal);
            if (queryParam is AccountParam)
            {
                var param = queryParam as AccountParam;
                if (param.Type != null)
                {
                    result = result.Where(p => p.Type == param.Type);
                }
                if (!string.IsNullOrEmpty(param.Account))
                {
                    result = result.Where(p => p.Account1 == param.Account);
                }
                result = result.Where(p => p.CreateTime >= param.StartTime && p.CreateTime <= param.EndTime);

            }
            return result;
        }

        /// <summary>
        /// 检测帐号名是否可用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckAccountUseable(ChainAccountRequest model)
        {
            var entity = DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.ChainCode == model.ChainCode && p.Account1 == model.Account);
            var order = DbContext.GetDbSet<Account>().FirstOrDefault(p =>
                p.State == (int)EnumState.Normal && p.ChainCode == model.ChainCode && p.Account1 == model.Account);
            return (entity == null) && (order == null);
        }
        
        /// <summary>
        /// 按时间获取用户总数
        /// </summary>
        /// <param name="seachcreactetime"></param>
        /// <returns></returns>
        public int GetTodayAddedCount(bool seachcreactetime = true)
        {
            if (seachcreactetime)
            {
                var date = DateTime.UtcNow.Date;
                return DbSet.Where(p => p.State == (int)EnumState.Normal && p.CreateTime >= date).Count();
            }
            else
            {
                return DbSet.Where(p => p.State == (int)EnumState.Normal).Count();
            }
        }
    }
}


