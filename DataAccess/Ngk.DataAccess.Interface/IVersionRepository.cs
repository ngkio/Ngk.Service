using Thor.Framework.Repository.Relational;
using Version = Ngk.DataAccess.Entities.Version;

namespace Ngk.DataAccess.Interface
{
    public interface IVersionRepository : IRepository<Version>
    {
        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <param name="clientType">客户端类型，1、Web，2、IOS,3、Android</param>
        /// <returns></returns>
        Version GetCurrentVersion(int clientType);
    }
}

