using System.Threading.Tasks;
using Thor.Framework.Data.Model;

namespace Contract.Interface
{
    /// <summary>
    /// 链信息相关客户端接口
    /// </summary>
    public interface IInfoClient : IBaseClient
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        Task<ExcutedResult> GetInfo();


        /// <summary>
        /// 获取1kbEos价格
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetRamEosPrice(decimal n, int retryTime = 3);

        /// <summary>
        /// 测试速度
        /// </summary>
        /// <param name="time"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<ExcutedResult> TestNetSpeed(int time, string url);

        Task<ExcutedResult> TransactionBinToJson(string hexData);
    }
}