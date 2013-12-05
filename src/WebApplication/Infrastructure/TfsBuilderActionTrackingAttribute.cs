using System;
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
    }
}