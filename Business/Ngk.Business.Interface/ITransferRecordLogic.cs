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
        /// ���ת�˼�¼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult AddTransferRecord(AddTransferRecordRequest model);

        /// <summary>
        /// ��ѯת�ʼ�¼
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<TransferRecordModel> GetTransferRecordPage(TransferRecordParam param);

        /// <summary>
        /// ��ȡ�û����ת���ʺ�
        /// </summary>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        List<string> GetTransferAccount(string chainCode);
    }
}

