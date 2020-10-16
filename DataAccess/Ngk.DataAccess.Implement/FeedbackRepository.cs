using System;
using System.Linq;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }


        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <param name="model"></param>
        public void Insert(Feedback model)
        {
            DbContext.GetDbSet<Feedback>().Add(model);
        }

        /// <summary>
        /// 获取短信记录(分页)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResults<Feedback> GetQuery(FeedBackParams param)
        {
            param.SortName = "CreateTime";
            var result= AdvQuery(param);
            var dataInfo = result.Data;
            if (dataInfo!=null && dataInfo.Any())
            {
                if (!string.IsNullOrEmpty(param.Mobile))
                {
                    dataInfo = dataInfo.Where(l => l.Mobile == param.Mobile).ToList();
                }
                if (!string.IsNullOrEmpty(param.Link))
                {
                    dataInfo = dataInfo.Where(l => l.Link == param.Link).ToList();
                }
                if (param.Type!=default(int))
                {
                    dataInfo = dataInfo.Where(l => l.Type == param.Type).ToList();
                }
                if (param.BusinessState != default(int))
                {
                    dataInfo = dataInfo.Where(l => l.BusinessState == param.BusinessState).ToList();
                }
                if (param.StartCreateTime!=default(DateTime))
                {
                    dataInfo = dataInfo.Where(l => l.CreateTime >= param.StartCreateTime).ToList();
                }
                if (param.EndCreateTime != default(DateTime))
                {
                    dataInfo = dataInfo.Where(l => l.CreateTime <= param.EndCreateTime).ToList();
                }

                result.Data = dataInfo;
            }
            return result;
        }
    }
}


