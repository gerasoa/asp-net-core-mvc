using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data.SqlTypes;
using System.Reflection;
using System.Security.Claims;

namespace MyProject.Extensions
{
    public class CustomAuthorization
    {
        public static  bool ValidationUserClaims(HttpContext context, string claimName, string claimValue)
        {
            if (context.User.Identity == null)  throw new InvalidOperationException();

            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity == null) throw new InvalidOperationException();

            if(!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/login", ReturnUrl = context.HttpContext.Request.Path.ToString()}));
                return;
            }

            if(!CustomAuthorization.ValidationUserClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                // 401 - Unauthorized
                // 403  - Forbidden
                context.Result = new StatusCodeResult(403);
            }
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
