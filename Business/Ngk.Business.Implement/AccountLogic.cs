using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Contract;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.IoC;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;

namespace Ngk.Business.Implement
{
    public class AccountLogic : BaseBusinessLogic<Account, IAccountRepository>, IAccountLogic
    {
        private readonly ContractClientFactory _contractFactory;

        private readonly IConfigDataLogic _configDataLogic;
        private readonly ITokensLogic _tokensLogic;
        private readonly ICreateAccountRecordRepository _createAccountRecordRepository;
        private readonly IConfiguration _config;

        private readonly string chain = "NGK";

        private readonly string _filePreUrl;

        private IHostingEnvironment Environment { get; }

        #region ctor
        public AccountLogic(IAccountRepository repository, IConfigDataLogic configDataLogic,
            ICreateAccountRecordRepository createAccountRecordRepository, ITokensLogic tokensLogic,
            IHostingEnvironment env, IConfiguration config) : base(repository)
        {
            _contractFactory = (ContractClientFactory)AspectCoreContainer.CreateScope().Resolve(typeof(ContractClientFactory));
            _configDataLogic = configDataLogic;
            _tokensLogic = tokensLogic;
            _createAccountRecordRepository = createAccountRecordRepository;
            Environment = env;
            _config = config;
            _filePreUrl = _config["FilePreUrl"];
        }
        #endregion

        /// <summary>
        /// 创建钱包帐号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExcutedResult CreateFreeAccount(CreateFreeAccountRequest request)
        {
            if (!RegexHelper.CheckAccount(request.Account))
            {
                return ExcutedResult.FailedResult(BusinessResultCode.AccountNotInRule, "帐号不符合规则");
            }
            if (request.PublicKey.Substring(0, 3) != chain)
            {
                throw new BusinessException(BusinessResultCode.PublicKeyError, "公钥格式错误");
            }
            //获取用户创建数
            int count = _createAccountRecordRepository.GetFreeCreateNum(chain, request.Uuid);
            if (int.TryParse(_configDataLogic.GetByKey(ConfigDataKey.RegisterAccountLimit), out var freeNum) && freeNum > 0)
            {
                if (count > freeNum)
                {
                    throw new BusinessException(BusinessResultCode.FreeAccountLimit, "您已无法创建账号，请联系客服人员");
                }
            }
            var client = _contractFactory.GetService<IAccountClient>(chain);
            var tx = client.CreateAccount(new NewAccount()
            {
                AccountName = request.Account,
                ActiveKey = request.PublicKey,
                OwnerKey = request.PublicKey,
            });
            tx.Wait();
            var result = tx.Result;
            if (result.Status != EnumStatus.Success)
            {
                if (result.Code != SysResultCode.ServerException)
                {
                    return ExcutedResult.FailedResult(result.Code, result.Message);
                }
                return ExcutedResult.FailedResult(BusinessResultCode.CreateAccountFail, "链创建账号失败");
            }
            //添加帐号及记录
            using (var trans = base.GetNewTransaction())
            {
                CreateAccountRecord record = new CreateAccountRecord()
                {
                    Owner = result.Data.Payer,
                    ChainCode = chain,
                    Account = request.Account,
                    ClientIp = CurrentUser.ClientIP,
                    Uuid = request.Uuid ?? "",
                    Type = (int)EnumAccountType.Free,
                    PublicKey = request.PublicKey,
                    Access = "active",
                    Remark = result.Data.TransactionId
                };
                _createAccountRecordRepository.Add(record);
                Account entity = new Account
                {
                    ChainCode = chain,
                    Account1 = request.Account,
                    Type = (int)EnumAccountType.Free,
                    PublicKey = request.PublicKey,
                    Access = "active",//TODO
                };
                Repository.Add(entity);
                trans.Commit();
            }
            return ExcutedResult.SuccessResult();
        }

        /// <summary>
        /// 检测帐号名是否可用
        /// </summary>
        /// <param name="model"></param>
        /// <returns>结果</returns>
        public ExcutedResult CheckAccountUseable(ChainAccountRequest model)
        {
            if (!RegexHelper.IsAccount(model.Account))
            {
                return ExcutedResult.FailedResult(BusinessResultCode.AccountNotInRule, "帐号不符合规则");
            }
            var flag = Repository.CheckAccountUseable(model);
            if (flag)
            {
                var contract = _contractFactory.GetService<IAccountClient>(model.ChainCode.ToUpper());
                var id = contract.GetAccountInfo(model.Account);
                id.Wait();
                var result = id.Result;
                if (result.Status == EnumStatus.Success)
                {
                    if (result.Data == null)
                    {
                        return ExcutedResult.SuccessResult();
                    }
                    else
                    {
                        return ExcutedResult.FailedResult(BusinessResultCode.AccountExist, "帐号已存在");
                    }
                }
                return new ExcutedResult(result.Status, result.Message, result.Code, null);
            }
            return ExcutedResult.FailedResult(BusinessResultCode.AccountExist, "帐号已存在");
        }
        
        /// <summary>
        /// 获取用户下所有帐号（分页）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResults<Account> GetAccountList(AccountParam param)
        {
            return AdvQuery(param);
        }
    }
}


