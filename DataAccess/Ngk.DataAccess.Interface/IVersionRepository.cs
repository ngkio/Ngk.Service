using Thor.Framework.Repository.Relational;
using Version = Ngk.DataAccess.Entities.Version;

namespace Ngk.DataAccess.Interface
{
    public interface IVersionRepository : IRepository<Version>
    {
        /// <summary>
        /// ��ȡ��ǰ�汾
        /// </summary>
        /// <param name="clientType">�ͻ������ͣ�1��Web��2��IOS,3��Android</param>
        /// <returns></returns>
        Version GetCurrentVersion(int clientType);
    }
}

