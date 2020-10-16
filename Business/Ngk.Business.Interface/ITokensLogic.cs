using System.Collections.Generic;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Manager;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface ITokensLogic : IBusinessLogic<Tokens>
    {
        /// <summary>
        /// �����Ż�ȡToken
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Tokens GetBySymbol(string chainCode, string symbol);

        /// <summary>
        /// ��ȡ��Լ�б�
        /// </summary>
        /// <returns></returns>
        List<GetTokenListResponse> GetTokenList(ChainModel model);

        /// <summary>
        /// ���Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult AddToken(TokenCreateModel model);

        /// <summary>
        /// ����Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult Edit(TokenEditModel model);
    }
}

