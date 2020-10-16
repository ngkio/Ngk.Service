using System;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Service.ManagerWebApi.Attribute;
using Ngk.Service.ManagerWebApi.Models;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    [Language]
    public class ManagerController : Controller
    {
        private readonly IConfigDataLogic _configDataLogic;
        private readonly IManagerLogic _managerLogic;

        public ManagerController(IManagerLogic managerLogic, IConfigDataLogic configDataLogic)
        {
            this._managerLogic = managerLogic;
            _configDataLogic = configDataLogic;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult UpdatePassWord([FromBody]UpdatePassWordModel model)
        {
            try
            {
                if (model == null
                    || String.IsNullOrEmpty(model.OldPassword)
                    || String.IsNullOrEmpty(model.NewPassword)
                    || String.IsNullOrEmpty(model.ConfirmPassword))
                {
                    throw new BusinessException("UE0001", "请核对参数！");
                }
                else if (model.ConfirmPassword != model.NewPassword)
                {
                    throw new BusinessException("UE0002", "确认密码和新密码不匹配！");
                }
                else
                {
                    bool isSuccess = _managerLogic.UpdatePassword(model.OldPassword, model.NewPassword);
                    return ExcutedResult.SuccessResult(isSuccess);
                }
            }
            catch (BusinessException bex)
            {
                return ExcutedResult.FailedResult(bex.ErrorCode, bex.Message);
            }
            catch (Exception)
            {
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "");
            }
        }
    }
}