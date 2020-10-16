using System;
using System.Threading.Tasks;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ngk.Service.ManagerWebApi.Common
{
    public class ApiAuthorizeFilter : AuthorizeFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly PrincipalUser _currentUser;

        public ApiAuthorizeFilter(AuthorizationPolicy policy, IHttpContextAccessor httpContextAccessor,PrincipalUser user) : base(policy)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUser = user;
        }

        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                try
                {
                    _currentUser.SetClaimsPrincipal(context.HttpContext.User);
                    _currentUser.ClientIP = _httpContextAccessor.HttpContext.GetRequestIp4Address()?.ToString();
                }
                catch (Exception)
                {
                    await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }

            await base.OnAuthorizationAsync(context);
        }
    }
}