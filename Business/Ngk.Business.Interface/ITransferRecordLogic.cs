using System.Collections.Generic;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface ITransferRecordLogic : IBusinessLogic<TransferRecord>
    {
        /// <summary>
        /// 添加转账记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult AddTransferRecord(AddTransferRecordRequest model);

        /// <summary>
        /// 查询转帐记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<TransferRecordModel> GetTransferRecordPage(TransferRecordParam param);

        /// <summary>
        /// 获取用户相关转帐帐号
        /// </summary>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        List<string> GetTransferAccount(string chainCode);
    }
}

