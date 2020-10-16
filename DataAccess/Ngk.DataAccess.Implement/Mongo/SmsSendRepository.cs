using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{

    public class SmsSendRepository : MongoRepository<SmsSend>, ISmsSendRepository
    {

        public SmsSendRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {

        }

        /// <summary>
        /// 添加短信日志日志
        /// </summary>
        /// <param name="model"></param>
        public void InsertSmsSend(SmsSend model)
        {

            bool result;
            IsCheckSmsSend(model.Id, out result);
            if (result)//存在 false 不存在 true
                return;
            try
            {
                Insert(model);
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex);


            }

        }

        /// <summary>
        /// 判断当前数据是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void IsCheckSmsSend(Guid guid, out bool result)
        {
            var fileHeader = FirstOrDefault(p => p.Id == guid);

            if (fileHeader == null)
                result = true;
            result = false;
        }

        /// <summary>
        /// 获取短信记录(分页)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResults<SmsSend> GetQuery(SmsSendParam param)
        {
            Expression<Func<SmsSend, bool>> queryExp = p => true;

            if (!string.IsNullOrWhiteSpace(param.Mobile))
                queryExp = queryExp.And(p => p.Mobile == param.Mobile);

            queryExp = queryExp.And(p => p.CreateTime >= param.StartTime && p.CreateTime <= param.EndTime);


            param.SortList = new Dictionary<string, bool> {

                    {param.SortName,param.IsSortOrderDesc}
            };

            return QueryPagedResults(queryExp, param);

        }
    }
}
