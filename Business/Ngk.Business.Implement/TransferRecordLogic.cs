using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Newtonsoft.Json;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class TransferRecordLogic : BaseBusinessLogic<TransferRecord, ITransferRecordRepository>, ITransferRecordLogic
    {
        private readonly IContactsRepository _contactsRepository;

        #region ctor
        public TransferRecordLogic(ITransferRecordRepository repository, IContactsRepository contactsRepository) : base(repository)
        {
            _contactsRepository = contactsRepository;
        }
        #endregion

        /// <summary>
        /// 添加转账记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult AddTransferRecord(AddTransferRecordRequest model)
        {
            TransferRecord record = new TransferRecord
            {
                UserId = CurrentUser.Id,
                TransferId = model.TransferId,
                ChainCode = model.ChainCode,
                TokenCode = model.TokenCode,
                Contract = model.Contract,
                FromAccount = model.FromAccount,
                ToAccount = model.ToAccount,
                Amount = model.Amount,
                BlockNum = model.BlockNum,
                Memo = model.Memo,
                TransferTime = model.TransferTime,
                CreateTime = DateTime.UtcNow
            };
            Create(record, out var excuted);
            return excuted;
        }

        /// <summary>
        /// 使用接口获取交易记录，如获取失败则查询数据库
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<TransferRecordModel> GetTransferRecordPage(TransferRecordParam param)
        {
            try
            {
                int skip = (param.PageIndex - 1) * param.PageSize;
                string url =
                    $"https://history.cryptolions.io/v1/history/get_actions/{param.Account}?skip={skip}&limit={param.PageSize}&sort=-1&filter=transfer";
                String returnMsg = HttpUtils.Get(url, "", null, null, 20 * 1000);
                var list = JsonConvert.DeserializeObject<TransferContractRepose>(returnMsg);
                var data = list.Data.Select(p => new TransferRecordModel
                {
                    Amount = p.Act.Data.Amount,
                    Contract = p.Act.Account,
                    CreateTime = p.Time,
                    FromAccount = p.Act.Data.From,
                    ToAccount = p.Act.Data.To,
                    Memo = p.Act.Data.Memo,
                    Symbol = p.Act.Data.Symbol,
                    TransferId = p.TransferId,
                    BlockNum = p.BlockNum
                }).Distinct().ToList();
                return data;
            }
            catch (Exception e)
            {
                param.IsSortOrderDesc = true;
                param.SortName = "TransferTime";
                var data = AdvQuery(param);
                return data.Data.Select(
                    p => new TransferRecordModel()
                    {
                        Amount = p.Amount.ToString(),
                        BlockNum = p.BlockNum,
                        Contract = p.Contract,
                        CreateTime = p.CreateTime,
                        FromAccount = p.FromAccount,
                        Memo = p.Memo,
                        Symbol = p.TokenCode,
                        ToAccount = p.ToAccount,
                        TransferId = p.TransferId
                    }).ToList();
            }
        }

        /// <summary>
        /// 获取用户相关转帐帐号
        /// </summary>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        public List<string> GetTransferAccount(string chainCode)
        {
            var userId = CurrentUser.Id;
            var list = Repository.GetTransferAccount(userId, chainCode);
            var contacts = _contactsRepository.GetContactsAccount(userId, chainCode);
            list.AddRange(contacts);
            list = list.Distinct().ToList();
            return list;
        }

    }
}


