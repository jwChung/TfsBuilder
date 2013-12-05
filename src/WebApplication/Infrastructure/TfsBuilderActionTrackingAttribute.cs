using System;
using System.Configuration;
using GoogleAnalyticsTracker.Web.Mvc;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// An attribute to provide action tracking of Google analytics.
    /// </summary>
    [CLSCompliant(false)]
    public class TfsBuilderActionTrackingAttribute : ActionTrackingAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuilderActionTrackingAttribute"/> class.
        /// </summary>
        public TfsBuilderActionTrackingAttribute() : base(
            ConfigurationManager.AppSettings["GoogleAnalyticsTrackingId"],
            ConfigurationManager.AppSettings["GoogleAnalyticsTrackingDomain"])
        {
        }
    }
}