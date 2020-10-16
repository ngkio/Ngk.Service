using System;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO.Manager;
using Ngk.DataAccess.DTO.Param;
using Version = Ngk.DataAccess.Entities.Version;

namespace Ngk.Business.Interface
{
    public interface IVersionLogic : IBusinessLogic<Version>
    {
        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <param name="clientType">客户端类型，1、Web，2、IOS,3、Android</param>
        /// <returns></returns>
        Version GetCurrentVersion(int clientType);

        PagedResults<Version> GetQuery(VersionParam param);
       void  Update(VersionEditRequest entity, out ExcutedResult result);

        void DeleteLogic(Guid guid, out ExcutedResult result);

        int? GetMaxVersion(int ClientType, int Number);
    }
}

