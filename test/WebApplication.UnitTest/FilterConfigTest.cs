using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using GoogleAnalyticsTracker.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Jwc.TfsBuilder.WebApplication.Infrastructure;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication
{
    public class FilterConfigTest
    {
        [Spec]
        public void RegisterGlobalFiltersRegistersCorrectFilters(GlobalFilterCollection filters)
        {
            Assert.Empty(filters);

            FilterConfig.RegisterGlobalFilters(filters);

            Assert.Equal(
                new[] { typeof(NotifyErrorAttribute), typeof(HandleErrorAttribute), typeof(ActionTrackingAttribute) },
                filters.Select(x => x.Instance.GetType()));
        }

        [Spec]
        public void RegisterGlobalFiltersRegistersCorrectNotifyErrorAttribute(
            GlobalFilterCollection filters)
        {
            // Arrange
            // Act
            FilterConfig.RegisterGlobalFilters(filters);

            // Assert
            var notifyErrorAttribute = filters.Select(f => f.Instance).OfType<NotifyErrorAttribute>().Single();
            Assert.IsType<EmailLogger>(notifyErrorAttribute.Logger);
        }

        [Spec]
        public void RegisterGlobalFiltersRegistersCorrectActionTrackingAttribute(
            GlobalFilterCollection filters)
        {
            // Arrange
            // Act
            FilterConfig.RegisterGlobalFilters(filters);

            // Assert
            var actionTrackingAttribute = filters.Select(f => f.Instance).OfType<ActionTrackingAttribute>().Single();
            Assert.Equal(
                ConfigurationManager.AppSettings["GoogleAnalyticsTrackingId"],
                actionTrackingAttribute.Tracker.TrackingAccount);
            Assert.Equal(
                ConfigurationManager.AppSettings["GoogleAnalyticsTrackingDomain"],
                actionTrackingAttribute.Tracker.TrackingDomain);
        }
    }
}