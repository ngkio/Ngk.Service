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
        /// ����ѯ����������������
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IQueryable<Tokens> QueryList(TokenParam param);


        /// <summary>
        /// �����Ż�ȡToken
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Tokens GetTokenBySymbol(string chainCode, string symbol);

        /// <summary>
        ///  ��ҳ��ѯ����
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResults<Tokens> GetQuery(TokenParam param);
    }
}

