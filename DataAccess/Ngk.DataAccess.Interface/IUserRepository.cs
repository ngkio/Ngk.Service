using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 通过手机获取用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        User GetUserByMobile(string mobile);

        /// <summary>
        /// 通过邀请码获取用户
        /// </summary>
        /// <param name="inviteCode">邀请码</param>
        /// <returns></returns>
        User GetUserByInviteCode(string inviteCode);
    }
}

