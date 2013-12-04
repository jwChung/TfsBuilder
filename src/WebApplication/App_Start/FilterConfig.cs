using System;
using System.Web.Mvc;
using Jwc.TfsBuilder.WebApplication.Infrastructure;

namespace Jwc.TfsBuilder.WebApplication
{
    public class FilterConfig
    {
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