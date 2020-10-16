using System;
using System.Linq;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class TokensRepository : BaseRepository<Tokens>, ITokensRepository
    {
        public TokensRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public override int Add(Tokens entity, bool withTrigger = false)
        {
            if (!CheckExistToken(entity))
            {
                entity.CreateTime = DateTime.UtcNow;
                return base.Add(entity, withTrigger);
            }
            return 1;
        }

        public override Tokens GetSingle(Guid key)
        {
            return DbSet.FirstOrDefault(p => p.Id == key && p.State == (int)EnumState.Normal);
        }

        /// <summary>
        /// 重写高级查询
        /// </summary>
        /// <typeparam name="TQueryParam"></typeparam>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        protected override IQueryable<Tokens> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam).Where(p => p.State == (int)EnumState.Normal);
            if (queryParam is TokenParam)
            {
                var param = queryParam as TokenParam;
                if (!string.IsNullOrEmpty(param.ChainCode))
                {
                    result = result.Where(p => p.ChainCode == param.ChainCode);
                }
                if (!string.IsNullOrEmpty(param.Name))
                {
                    result = result.Where(p => p.Name.Contains(param.Name) || p.Symbol.Contains(param.Name));
                }
                if (!string.IsNullOrEmpty(param.Issuer))
                {
                    result = result.Where(p => p.Issuer == param.Issuer);
                }
                //是否主流
                if (param.IsMain != null)
                {
                    result = result.Where(p => p.IsMain == param.IsMain);
                }

                //MONGO数据的验证
                if (param.Keys != null && param.Keys.Any() && param.IsExist.HasValue)
                {
                    if (param.IsExist.Value)
                    {
                        result = result.Where(p => param.Keys.Contains($"{p.Symbol.ToLower()}_{p.Contract.ToLower()}"));
                    }
                    else
                    {
                        result = result.Where(p => !param.Keys.Contains($"{p.Symbol.ToLower()}_{p.Contract.ToLower()}"));
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 按查询参数返回所有数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IQueryable<Tokens> QueryList(TokenParam param)
        {
            var query = GetAdvQuery(param);
            return query;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResults<Tokens> GetQuery(TokenParam param)
        {
            var query = GetAdvQuery(param);
            return query.ToPagedResults<Tokens, Tokens>(param);
        }

        /// <summary>
        /// 按符号获取Token
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public Tokens GetTokenBySymbol(string chainCode, string symbol)
        {
            var token = DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.ChainCode == chainCode && p.Symbol == symbol);
            return token;
        }

        /// <summary>
        /// 检查是否存在相同TOKEN
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private bool CheckExistToken(Tokens tokens)
        {
            var entity = DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.ChainCode == tokens.ChainCode && p.Symbol == tokens.Symbol && p.Name == tokens.Name && p.Issuer == tokens.Issuer);
            return entity != null;
        }
    }
}


