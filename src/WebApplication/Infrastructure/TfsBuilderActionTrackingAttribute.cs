using System;
using GoogleAnalyticsTracker.Web.Mvc;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// An attribute to provide action tracking of Google analytics.
    /// </summary>
    [CLSCompliant(false)]
    public class TfsBuilderActionTrackingAttribute : ActionTrackingAttribute
    {
    }
}