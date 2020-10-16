using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Mongo;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.Interface
{
  
        public interface ISmsSendRepository : IMongoRepository<SmsSend>
        {
        void InsertSmsSend(SmsSend model);
        PagedResults<SmsSend> GetQuery(SmsSendParam param);


        }
}
