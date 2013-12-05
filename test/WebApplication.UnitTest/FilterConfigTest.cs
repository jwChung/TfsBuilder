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
    }
}