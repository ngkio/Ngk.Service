using System;
using System.Collections.Generic;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        /// <summary>
        /// 检测帐号名是否可用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CheckAccountUseable(ChainAccountRequest model);

        /// <summary>
        /// 获取今天注册的账户
        /// </summary>
        /// <returns></returns>
        int GetTodayAddedCount(bool seachcreactetime = true);
        
    }
}

