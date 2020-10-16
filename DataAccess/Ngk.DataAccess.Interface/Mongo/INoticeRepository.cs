using System;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.Interface.Mongo
{
    /// <summary>
    /// mongo的公告数据
    /// </summary>
    public interface INoticeRepository : IMongoRepository<Notice>
    {
        /// <summary>
        /// 获取指定服务名称下比参数时间大的下一条数据
        /// </summary>
        /// <param name="time"></param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        Notice GetNextInfoByTime(DateTime time, string serviceName);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResults<Notice> GetQuery(NoticeParams model);
    }
}
