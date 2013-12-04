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
        private readonly IEquatable<Exception> _condition;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyErrorAttribute"/> class.
        /// </summary>
        /// <param name="logger">A logger.</param>
        /// <param name="condition">A condition to log an exception message.</param>
        public NotifyErrorAttribute(EmailLogger logger, IEquatable<Exception> condition)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            _logger = logger;
            _condition = condition;
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

        /// <summary>
        /// Gets a value indicating condition to log an exception message passed from the constructor.
        /// </summary>
        public IEquatable<Exception> Condition
        {
            get
            {
                return _condition;
            }
        }

        /// <inheritdoc/>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!Condition.Equals(filterContext.Exception))
            {
                return;
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