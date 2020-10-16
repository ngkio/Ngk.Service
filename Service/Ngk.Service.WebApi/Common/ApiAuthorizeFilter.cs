using System.Threading.Tasks;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ngk.Service.WebApi.Common
{
    public class ApiAuthorizeFilter : AuthorizeFilter
    {
        private readonly PrincipalUser _currentUser;
        public ApiAuthorizeFilter(AuthorizationPolicy policy, PrincipalUser principalUser) : base(policy)
        {
            _currentUser = principalUser;
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                _currentUser.SetClaimsPrincipal(context.HttpContext.User);
            }

            return base.OnAuthorizationAsync(context);
        }
    }
}
