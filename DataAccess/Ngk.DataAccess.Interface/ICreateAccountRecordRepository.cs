using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface ICreateAccountRecordRepository : IRepository<CreateAccountRecord>
    {
        /// <summary>
        /// 获取今天IP免费注册数
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        int CheckIpRegisterNum(string ip);

        /// <summary>
        /// 获取创建数
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        int GetFreeCreateNum(string chainCode, string uuid);
    }
}

