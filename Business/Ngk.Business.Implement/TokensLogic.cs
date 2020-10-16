using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Microsoft.Extensions.Configuration;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Manager;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class TokensLogic : BaseBusinessLogic<Tokens, ITokensRepository>, ITokensLogic
    {
        private readonly string _filePreUrl;
        private readonly IConfigDataLogic _configDataLogic;


        #region ctor

        public TokensLogic(ITokensRepository repository,
            IConfiguration configuration, IConfigDataLogic configDataLogic) : base(repository)
        {
            _filePreUrl = configuration["FilePreUrl"];
            _configDataLogic = configDataLogic;
        }
        #endregion

        public override void Delete(Guid id, out ExcutedResult result)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "数据不存在，请刷新");
                return;
            }

            entity.DeleteIp = CurrentUser.ClientIP;
            entity.State = (int)EnumState.Deleted;
            entity.DeleteTime = DateTime.UtcNow;
            entity.Deleter = CurrentUser.UserName;
            base.Update(entity, out result);
            result.Message = result.Status == EnumStatus.Success ? "删除成功" : "删除失败";
        }


        /// <summary>
        /// 添加Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult AddToken(TokenCreateModel model)
        {
            try
            {
                using (var trans = GetNewTransaction())
                {
                    Tokens entity = new Tokens()
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        Symbol = model.Symbol,
                        Issuer = model.Issuer,
                        Precision = model.Precision,
                        Desc = model.Desc,
                        Logo = model.Logo,
                        Order = model.Order,
                        ChainCode = "EOS",
                        Contract = model.Contract,
                        CreateTime = DateTime.UtcNow,


                    };
                    if (model.IsNeedAudit)
                    {
                        entity.State = (int)EnumState.Deleted;
                    }
                    else
                    {
                        entity.State = (int)EnumState.Normal;
                    }
                    Create(entity, out var result);
                    trans.Commit();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex);
                return ExcutedResult.FailedResult(BusinessResultCode.SubmitTokenError, "提交TOKEN错误");
            }
        }

        /// <summary>
        /// 更新Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult Edit(TokenEditModel model)
        {
            Tokens entity = GetById(model.Id);
            if (entity == null)
            {
                return ExcutedResult.FailedResult(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
            }
            entity.Name = model.Name;
            entity.Symbol = model.Symbol;
            entity.Issuer = model.Issuer;
            entity.Precision = model.Precision;
            entity.Desc = model.Desc;
            entity.Logo = model.Logo;
            entity.Order = model.Order;
            entity.IsSystem = model.IsSystem;
            entity.Contract = model.Contract;
            Update(entity, out var result);
            return result;
        }

        /// <summary>
        /// 按符号换取Token
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public Tokens GetBySymbol(string chainCode, string symbol)
        {
            return Repository.GetSingleOrDefault(p => p.ChainCode == chainCode && p.Symbol == symbol.ToUpper() && p.State == (int)EnumState.Normal);
        }

        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <returns></returns>
        public List<GetTokenListResponse> GetTokenList(ChainModel model)
        {
            if (string.IsNullOrEmpty(model.ChainCode))
            {
                throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误");
            }
            var param = new TokenParam
            {
                ChainCode = model.ChainCode
            };
            var list = Repository.QueryList(param).ToList();
            if (CurrentUser.Id != Guid.Empty)
            {
                var favoriteParam = new UserFavoriteParam
                {
                    UserId = CurrentUser.Id,
                    ItemType = (int)EnumItemType.Token,
                    RelatedIds = list.Select(p => p.Id).ToList()
                };
            }

            var result = list.OrderByDescending(p => p.IsSystem).ThenByDescending(p => p.Order).Select(p =>
                new GetTokenListResponse()
                {
                    Name = p.Name,
                    Symbol = p.Symbol,
                    Issuer = p.Issuer,
                    Contract = p.Contract,
                    Logo = p.Logo.HasValue ? _filePreUrl + p.Logo.Value : "",
                    IsSystem = p.IsSystem,
                    DollarPrice = p.DollarPrice,
                    FaceBookUrl = p.FaceBookUrl,
                    IssueCost = p.IssueCost,
                    IssueDate = p.IssueDate,
                    IssueState = p.IssueState,
                    RmbPrice = p.RmbPrice,
                    TwitterUrl = p.TwitterUrl,
                    WebSite = p.WebSite,
                    WhitePaperUrl = p.WhitePaperUrl,
                    Decimals = p.Precision,
                    Precision = p.TransactionPrecision

                }).ToList();
            return result;
        }
    }
}


