using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class TransferRecordRepository : BaseRepository<TransferRecord>, ITransferRecordRepository
    {
        public TransferRecordRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<TransferRecord> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam);
            if (queryParam is TransferRecordParam)
            {
                var param = queryParam as TransferRecordParam;
                if (!string.IsNullOrEmpty(param.ChainCode))
                {
                    result = result.Where(p => p.ChainCode == param.ChainCode);
                }
                if (!string.IsNullOrEmpty(param.TokenCode))
                {
                    result = result.Where(p => p.TokenCode.Contains(param.TokenCode));
                }
                if (!string.IsNullOrEmpty(param.TransferId))
                {
                    result = result.Where(p => p.TransferId == param.TransferId);
                }
                if (!string.IsNullOrEmpty(param.Account))
                {
                    result = result.Where(p => p.FromAccount == param.Account || p.ToAccount == param.Account);
                }

                if (!string.IsNullOrEmpty(param.Contract))
                {
                    result = result.Where(p => p.Contract.Contains(param.Contract));
                }

                if (queryParam.StartTime != default(DateTime))
                {
                    result = result.Where(p => p.CreateTime >= queryParam.StartTime);
                }

                if (queryParam.EndTime != default(DateTime))
                {
                    if (queryParam.EndTime == queryParam.StartTime)
                    {
                        queryParam.EndTime = queryParam.EndTime.AddDays(1).AddMilliseconds(-1);
                    }
                    result = result.Where(p => p.CreateTime <= queryParam.EndTime);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取用户相关转帐帐号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        public List<string> GetTransferAccount(Guid userId, string chainCode)
        {
            var query = DbSet.Where(p => p.UserId == userId && p.ChainCode == chainCode);
            var list1 = query.Select(p => p.FromAccount).Distinct();
            var list2 = query.Select(p => p.ToAccount).Distinct();
            var count = list1.Union(list2).Distinct();
            var result = count.ToList();
            return result;
        }

        /// <summary>
        /// 分页查询转账记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedResults<TransferRecord> GetTransferRecord(TransferRecordParam model)
        {
            var iQueryable = GetAdvQuery(model);
            return iQueryable.ToPagedResults<TransferRecord, TransferRecord>(model);
        }
    }
}


