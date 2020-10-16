using System;
using System.Linq;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public override User GetSingle(Guid key)
        {
            return DbSet.FirstOrDefault(p => p.Id == key && p.State == (int)EnumState.Normal);
        }

        protected override IQueryable<User> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam).Where(p => p.State == (int)EnumState.Normal);
            if (queryParam is UserParam)
            {
                var param = queryParam as UserParam;
                if (!string.IsNullOrEmpty(param.Mobile))
                {
                    result = result.Where(p => p.Mobile == param.Mobile);
                }

                if (param.Type.HasValue)
                {
                    result = result.Where(p => p.UserType == param.Type.Value);
                }
            }
            return result;
        }

        /// <summary>
        /// 通过手机获取用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public User GetUserByMobile(string mobile)
        {
            return DbSet.FirstOrDefault(p => p.Mobile == mobile && p.State == (int)EnumState.Normal);
        }


        /// <summary>
        /// 通过邀请码获取用户
        /// </summary>
        /// <param name="inviteCode">邀请码</param>
        /// <returns></returns>
        public User GetUserByInviteCode(string inviteCode)
        {
            return DbSet.FirstOrDefault(p => p.InviteCode.ToUpper() == inviteCode.ToUpper() && p.State == (int)EnumState.Normal);
        }

    }
}


