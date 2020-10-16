using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface ILoginLogLogic : IBusinessLogic<LoginLog>
    {
        /// <summary>
        /// 查询登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryLoginLog(LoginLogParam model);
    }
}
