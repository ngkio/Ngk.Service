namespace Ngk.DataAccess.Interface
{
    public interface IRandomIdRepository
    {
        /// <summary>
        /// 获取随机合约编号
        /// </summary>
        /// <returns>编号</returns>
        long GetContractNum();
    }
}
