using System;
using System.Web.Mvc;
using GoogleAnalyticsTracker.Web.Mvc;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// An attribute to provide action tracking of Google analytics.
    /// </summary>
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class TfsBuilderActionTrackingAttribute : ActionTrackingAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuilderActionTrackingAttribute"/> class.
        /// </summary>
        public TfsBuilderActionTrackingAttribute()
            : base((string)AppSettings.Instance.GoogleAnalyticsTrackingId)
        {
        }

        /// <summary>
        /// Builds the current action URL.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <returns></returns>
        public override string BuildCurrentActionUrl(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            return base.BuildCurrentActionUrl(filterContext);
        }
    }
}