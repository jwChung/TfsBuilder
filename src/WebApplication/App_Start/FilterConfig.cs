using System;
using System.Web.Mvc;
using Jwc.TfsBuilder.WebApplication.Infrastructure;

namespace Jwc.TfsBuilder.WebApplication
{
    /// <summary>
    /// Represents global filter configurations.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers global filters.
        /// </summary>
        /// <param name="filters">Global fileters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NotifyErrorAttribute(
                logger: new EmailLogger(),
                condition: new ExceptionSpecification(GetExceptionSpecification)));

            filters.Add(new HandleErrorAttribute());
        }

        private static bool GetExceptionSpecification(Exception e)
        {
            return !(e is ArgumentException) && !(e is TfsBuildException);
        }
    }
}