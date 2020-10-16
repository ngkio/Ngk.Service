using System.Collections.Generic;
using System.Threading.Tasks;
using Ngk.DataAccess.DTO.Contract;
using Thor.Framework.Data.Model;

namespace Contract.Interface
{
    /// <summary>
    /// 链账户相关客户端接口
    /// </summary>
    public interface IAccountClient : IBaseClient
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account">账户名</param>
        Task<ExcutedResult<AccountInfo>> GetAccountInfo(string account);

        /// <summary>
        /// 通过key获取账号
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="retryTime">重试次数，默认3次</param>
        /// <returns>账号列表</returns>
        Task<IList<KeyRelateAccount>> GetAccountsByKey(string publicKey, int retryTime = 3);

        /// <summary>
        /// 创建新账户
        /// </summary>
        /// <param name="newAccount">新账户信息</param>
        /// <returns>操作结果</returns>
        Task<ExcutedResult<NewAccountResult>> CreateAccount(NewAccount newAccount);
    }
}