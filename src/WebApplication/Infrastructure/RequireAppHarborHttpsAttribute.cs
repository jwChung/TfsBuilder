using System;
using System.Web.Mvc;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents HTTPs filter only used on AppHarbor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RequireAppHarborHttpsAttribute : RequireHttpsAttribute
    {
        /// <inheritdoc/>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (IsLocalRequest(filterContext) || IsFowardedProtocalHttps(filterContext))
            {
                return;
            }

            base.OnAuthorization(filterContext);
        }

        private static bool IsLocalRequest(AuthorizationContext filterContext)
        {
            return filterContext.HttpContext.Request.IsLocal;
        }

        private static bool IsFowardedProtocalHttps(AuthorizationContext filterContext)
        {
            return string.Equals(
                filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"],
                "https",
                StringComparison.OrdinalIgnoreCase);
        }
    }
}