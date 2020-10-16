using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities;
using Pangu.ServiceCenter.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IPanguLogic
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult InsertNoticeInfo(NoticeRequestModel model);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExcutedResult DeleteNoticeInfo(string id);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult UpdateNoticeInfo(UpdateNoticeRequestModel model);

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryNoticeInfo(QueryNoticeRequestModel model);
    }
}
