using System.Linq;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class VersionRepository : BaseRepository<Version>, IVersionRepository
    {
        public VersionRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Version> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam).Where(p => p.State == (int)EnumState.Normal );
            if (queryParam is VersionParam)
            {
                var param = queryParam as VersionParam;

                if (!string.IsNullOrEmpty(param.Name))
                {
                    result = result.Where(p => p.Name.Contains(param.Name));
                }
                if (param.ClientType != null)
                {
                    result = result.Where(p => p.ClientType == param.ClientType);
                }
                if (param.Number!=null)
                {
                    result = result.Where(p => p.Number == param.Number);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <param name="clientType">客户端类型，1、Web，2、IOS,3、Android</param>
        /// <returns></returns>
        public Version GetCurrentVersion(int clientType)
        {
            var version = DbSet.Where(p => p.State == (int)EnumState.Normal && p.ClientType == clientType).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return version;
        }
        

    }
}


