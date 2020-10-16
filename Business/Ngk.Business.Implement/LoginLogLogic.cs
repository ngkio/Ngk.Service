using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class LoginLogLogic : BaseBusinessLogic<LoginLog, ILoginLogRepository>, ILoginLogLogic
    {
        #region ctor
        public LoginLogLogic(ILoginLogRepository repository) : base(repository)
        {

        }
        #endregion

        public ExcutedResult QueryLoginLog(LoginLogParam model)
        {
            try
            {
                if (model == null) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                if (String.IsNullOrEmpty(model.SortName))
                {
                    model.SortName = "CreateTime";
                    model.IsSortOrderDesc = true;
                    model.SortList = new Dictionary<string, bool> {
                        {model.SortName,model.IsSortOrderDesc}
                    };
                }
                var dataInfo = Repository.GetQuery(model);
                return ExcutedResult.SuccessResult(dataInfo);

            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
            catch (Exception exception)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "网络异常，请稍后重试或联系管理员！");
            }
        }
    }
}
