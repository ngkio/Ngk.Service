using Thor.Framework.Common.Pager;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class SmsSendLogic : ISmsSendLogic
    {
        private readonly ISmsSendRepository _smsSendRepository;

        public SmsSendLogic(ISmsSendRepository smsSendRepository)
        {
            _smsSendRepository = smsSendRepository;
        }

        public PagedResults<SmsSend> GetQuery(SmsSendParam param) {
            return _smsSendRepository.GetQuery(param);



        }
        
    }
}
