using System;
using System.Globalization;
using System.Web.Mvc;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Defines an attribute to notify an exception message.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [CLSCompliant(false)]
    public sealed class NotifyErrorAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly EmailLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyErrorAttribute"/> class.
        /// </summary>
        /// <param name="logger">A logger.</param>
        public NotifyErrorAttribute(EmailLogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _logger = logger;
        }

        /// <summary>
        /// Gets a value indicating the logger.
        /// </summary>
        public EmailLogger Logger
        {
            get
            {
                return _logger;
            }
        }

        /// <inheritdoc/>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            Logger.Log(
                subject: string.Format(
                    CultureInfo.CurrentCulture,
                    "TfsBuilder - {0}",
                    filterContext.Exception.GetType().Name),
                body: filterContext.Exception.ToString());

            filterContext.Result = new HttpNotFoundResult();
            filterContext.ExceptionHandled = true;
        }
    }
}