using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Interface;
using Contract.Interface.Model;
using Thor.Framework.Common.Helper;
using Thor.Framework.Data.Model;
using Thor.Framework.Ext.NGKSharp.Core.Exceptions;

namespace Contract.Implement.Ngk
{
    /// <summary>
    /// EOS的通证操作客户端
    /// </summary>
    public class TokenClient : BaseClient, ITokenClient
    {
        /// <summary>
        /// 获取通证余额
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="currency">通证符号</param>
        /// <returns>余额</returns>
        public async Task<decimal> GetBalanceAsync(string account, string currency, string tokenAccount = "")
        {
            try
            {
                var response = await Client.GetCurrencyBalance(tokenAccount, account, currency);
                if (response.Any())
                    return decimal.Parse(response.First().Split(" ")[0]);
                return 0;
            }
            catch (ApiErrorException e)
            {
                Log4NetHelper.WriteError(GetType(), e,
                    $"Point:{Point.HttpAddress} Code:{e.code} ErrorName:{e.error.name} Error:{e.error.what} \naccount:{account}\ncurrency:{currency}\ntokenAccount:{tokenAccount}");
                throw;
            }
            catch (ApiException ex)
            {
                NodeRepository.ApiException();
                Log4NetHelper.WriteError(typeof(TokenClient), ex, $"StatusCode:{ex.StatusCode} Content:{ex.Content}");
                return 0;
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress}");
                return 0;
            }
        }
    }
}