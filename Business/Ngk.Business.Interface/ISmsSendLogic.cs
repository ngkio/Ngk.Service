using System.Collections.Generic;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.Business.Interface
{
   public interface ISmsSendLogic
    {
        PagedResults<SmsSend> GetQuery(SmsSendParam param);
    }
}
