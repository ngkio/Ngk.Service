using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Pangu.ServiceCenter.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IPanguRepository
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
        ExcutedResult UpdateNoticeInfo(UpdateNoticeRequestModel model, out Notice oldNotice);

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryNoticeInfo(QueryNoticeRequestModel model);
    }
}
