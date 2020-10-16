using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Interface.Model;
using Thor.Framework.Data.Model;

namespace Contract.Interface
{
    /// <summary>
    /// 通证相关客户端接口
    /// </summary>
    public interface ITokenClient : IBaseClient
    {
        /// <summary>
        /// 获取通证余额
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="currency">通证符号</param>
        /// <param name="tokenAccount">通证发行账号</param>
        /// <returns>余额</returns>
        Task<decimal> GetBalanceAsync(string account, string currency, string tokenAccount = "");
    }
}