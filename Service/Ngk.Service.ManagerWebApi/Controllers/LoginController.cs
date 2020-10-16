using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using Ngk.Business.Interface;
using Ngk.Common.Enum;
using Ngk.DataAccess.Entities;
using Ngk.Service.ManagerWebApi.Attribute;
using Ngk.Service.ManagerWebApi.Models;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IManagerLogic _managerLogic;
        private readonly ILoginLogLogic _logLogic;

        public LoginController(IManagerLogic managerLogic, ILoginLogLogic logLogic)
        {
            _managerLogic = managerLogic;
            _logLogic = logLogic;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ExcutedResult> SignIn([FromBody] SignInModel model)
        {
            try
            {
                var result = _managerLogic.SignIn(model.UserName, model.Password, model.AuthenticateNum);
                if (result.Status == EnumStatus.Success)
                {
                    var managerInfo = (PrincipalUser)result.Data;
                    //用户标识
                    var identity = new ClaimsIdentity("Cookie", JwtClaimTypes.Name, JwtClaimTypes.Role);
                    identity.AddClaim(new Claim(JwtClaimTypes.Id, managerInfo.Id.ToString()));
                    identity.AddClaim(new Claim(JwtClaimTypes.Name, managerInfo.UserName));
                    identity.AddClaim(new Claim(JwtClaimTypes.Role, managerInfo.Role?.ToString()));

                    identity.AddClaim(new Claim("sub", managerInfo.Id.ToString()));
                    identity.AddClaim(new Claim("baseInfo", JsonConvert.SerializeObject(managerInfo)));

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));

                    return ExcutedResult.SuccessResult(managerInfo);
                }
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ExcutedResult> SignOut()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ExcutedResult CreateAccount(string account, string name, string mobile)
        {
            try
            {
                _managerLogic.Create(new Manager
                {
                    Account = account,
                    Name = name,
                    Mobile = mobile,
                    ManagerType = (int)EnumManagerType.Operator,
                }, out var result);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}
