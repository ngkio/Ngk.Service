using System;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IManagerLogic : IBusinessLogic<Manager>
    {
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        /// <param name="authenticateNum">��֤��</param>
        /// <returns>��¼������û���Ϣ</returns>
        ExcutedResult SignIn(string userName, string password, int authenticateNum);


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool UpdatePassword(String oldPassword, String newPassword);
    }
}

