using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Interface;
using Contract.Interface.Model;
using Newtonsoft.Json;
using Ngk.Common;
using Ngk.DataAccess.DTO.Contract;
using Thor.Ext.CryptoTool.Lib;
using Thor.Framework.Common.Helper;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Thor.Framework.Ext.NGKSharp.Core.Api.v1;
using Thor.Framework.Ext.NGKSharp.Core.Exceptions;
using Thor.Framework.Ext.NGKSharp.Core.Providers;
using Action = Thor.Framework.Ext.NGKSharp.Core.Api.v1.Action;

namespace Contract.Implement.Ngk
{
    public class AccountClient : BaseClient, IAccountClient
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account">账户名</param>
        public async Task<ExcutedResult<AccountInfo>> GetAccountInfo(string account)
        {
            try
            {
                var info = await Client.GetAccount(account);
                var accountInfo = MapperHelper<GetAccountResponse, AccountInfo>.Map(info);
                return ExcutedResult<AccountInfo>.SuccessResult(accountInfo);
            }
            catch (ApiErrorException aex)
            {
                Log4NetHelper.WriteError(GetType(), aex, $"Point:{Point.HttpAddress} Account:{account}");
                return ExcutedResult<AccountInfo>.SuccessResult("", null);
            }
            catch (ApiException ex)
            {
                NodeRepository.ApiException();
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress} StatusCode:{ex.StatusCode} Content:{ex.Content}");
                return ExcutedResult<AccountInfo>.FailedResult(BusinessResultCode.ChainRequestError, "EOS request error.");
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress} Account:{account}");
                return ExcutedResult<AccountInfo>.FailedResult(SysResultCode.ServerException, "");
            }
        }

        /// <summary>
        /// 通过key获取账号
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="retryTime">重试次数，默认3次</param>
        /// <returns>账号列表</returns>
        public async Task<IList<KeyRelateAccount>> GetAccountsByKey(string publicKey, int retryTime = 3)
        {
            try
            {
                var accountNameList = await Client.GetKeyAccounts(publicKey);
                var results = new List<KeyRelateAccount>();
                foreach (var accountName in accountNameList)
                {
                    var infoResult = await GetAccountInfo(accountName);
                    if (infoResult.Status != EnumStatus.Success)
                    {
                        throw new Exception();
                    }
                    var permissionList = new List<string>();
                    foreach (var permission in infoResult.Data.Permissions)
                    {
                        var auth = permission.RequiredAuth;
                        if (auth.Keys.Any(p =>
                            p.Weight == auth.Threshold &&
                            p.Key.Equals(publicKey, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            permissionList.Add(permission.PermName);
                        }
                    }
                    results.Add(new KeyRelateAccount { AccountInfo = infoResult.Data, PermissionNames = permissionList });
                }
                return results;
            }
            catch (Exception ex)
            {
                if (retryTime <= 0)
                    return null;
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress} publicKey:{publicKey} retryTime:{retryTime}");
                SwitchNode();
                return await GetAccountsByKey(publicKey, --retryTime);
            }
        }

        /// <summary>
        /// 创建新账户
        /// </summary>
        /// <param name="newAccount">新账户信息</param>
        /// <returns>操作结果</returns>
        public async Task<ExcutedResult<NewAccountResult>> CreateAccount(NewAccount newAccount)
        {
            try
            {
                var createContract = ConfigDataRepository.GetByKey(ConfigDataKey.CreateAccountContract);
                var createCreator = ConfigDataRepository.GetByKey(ConfigDataKey.CreateChainAccount);
                var createCreatorKey = ConfigDataRepository.GetByKey(ConfigDataKey.AgentAccountKey);
                //组装私钥(私钥解密)
                var aesKey = Configuration["AesKey"];
                var decryptKey = CryptographyHelper.Decrypt(createCreatorKey, aesKey, null);
                Configurator.SignProvider = new DefaultSignProvider(decryptKey);
                var owner = new Authority
                {
                    accounts = new List<AuthorityAccount>(),
                    keys = new List<AuthorityKey>() { new AuthorityKey() { key = newAccount.OwnerKey, weight = 1 } },
                    threshold = 1,
                    waits = new List<AuthorityWait>()
                };
                var active = new Authority
                {
                    accounts = new List<AuthorityAccount>(),
                    keys = new List<AuthorityKey>() { new AuthorityKey() { key = newAccount.ActiveKey, weight = 1 } },
                    threshold = 1,
                    waits = new List<AuthorityWait>()
                };

                var client = new Thor.Framework.Ext.NGKSharp.Ngk(Configurator);
                var action = new Action
                {
                    account = createContract,
                    authorization = new List<PermissionLevel>
                    {
                        new PermissionLevel{actor = createCreator, permission = "active"}
                    },
                    name = "newaccount",
                    data = new
                    {
                        creator = createCreator,
                        name = newAccount.AccountName,
                        owner = owner,
                        active = active
                    }
                };
                var str = JsonConvert.SerializeObject(action);
                Console.WriteLine(str);
                var trans = new Transaction
                {
                    actions = new List<Action>() { action }
                };
                var result = await client.CreateTransaction(trans);
                return ExcutedResult<NewAccountResult>.SuccessResult(new NewAccountResult { Payer = createContract, TransactionId = result });
            }
            catch (ApiErrorException ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress}\r\nStatusCode:{ex.code}\r\nContent:{JsonConvert.SerializeObject(ex.error)}");
                string msg = "";
                if (ex.error != null)
                {
                    foreach (var t in ex.error.details)
                    {
                        if (t.message.Contains("name is already taken"))
                        {
                            return ExcutedResult<NewAccountResult>.FailedResult(BusinessResultCode.AccountExist, "帐号已存在");
                        }
                        msg += t.message;
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    msg = ex.error?.what;
                }
                return ExcutedResult<NewAccountResult>.FailedResult(SysResultCode.ServerException, msg);
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress} Account:{JsonConvert.SerializeObject(newAccount)}");
                throw;
            }

        }
    }
}