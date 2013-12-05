using System;
using System.Web.Mvc;
using Jwc.TfsBuilder.WebApplication.Infrastructure;

namespace Jwc.TfsBuilder.WebApplication
{
    /// <summary>
    /// Represents global filter configurations.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    public class FilterConfig
    {
        /// <summary>
        /// Registers global filters.
        /// </summary>
        /// <param name="filters">Global fileters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters");
            }

            filters.Add(new NotifyErrorAttribute(new EmailLogger()));
            filters.Add(new HandleErrorAttribute());
        }
    }
}