using System.Linq;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface ITokensRepository : IRepository<Tokens>
    {
        /// <summary>
        /// 按查询参数返回所有数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IQueryable<Tokens> QueryList(TokenParam param);


        /// <summary>
        /// 按符号获取Token
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Tokens GetTokenBySymbol(string chainCode, string symbol);

        /// <summary>
        ///  分页查询数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResults<Tokens> GetQuery(TokenParam param);
    }
}

