using System;
using System.Collections.Generic;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface ITransferRecordRepository : IRepository<TransferRecord>
    {

        /// <summary>
        /// 获取用户相关转帐帐号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        List<string> GetTransferAccount(Guid userId, string chainCode);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResults<TransferRecord> GetTransferRecord(TransferRecordParam model);
    }
}

