using System;
using System.Collections.Generic;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.Model;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Contract;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IAccountLogic : IBusinessLogic<Account>
    {
        /// <summary>
        /// 检测帐号名是否可用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult CheckAccountUseable(ChainAccountRequest model);

        /// <summary>
        /// 创建钱包帐号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult CreateFreeAccount(CreateFreeAccountRequest model);

        /// <summary>
        /// 获取用户下所有帐号
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        PagedResults<Account> GetAccountList(AccountParam param);
    }
}

