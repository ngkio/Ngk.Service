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
        /// ͨ���ֻ���ȡ�û�
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        User GetUserByMobile(string mobile);

        /// <summary>
        /// ͨ���������ȡ�û�
        /// </summary>
        /// <param name="inviteCode">������</param>
        /// <returns></returns>
        User GetUserByInviteCode(string inviteCode);
    }
}

