using System;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IManagerLogic : IBusinessLogic<Manager>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="authenticateNum">验证码</param>
        /// <returns>登录结果及用户信息</returns>
        ExcutedResult SignIn(string userName, string password, int authenticateNum);


        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool UpdatePassword(String oldPassword, String newPassword);
    }
}

