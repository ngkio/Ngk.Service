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
        /// 按符号换取Token
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Tokens GetBySymbol(string chainCode, string symbol);

        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <returns></returns>
        List<GetTokenListResponse> GetTokenList(ChainModel model);

        /// <summary>
        /// 添加Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult AddToken(TokenCreateModel model);

        /// <summary>
        /// 更新Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult Edit(TokenEditModel model);
    }
}

