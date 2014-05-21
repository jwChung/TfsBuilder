using System.Linq;
using System.Web.Mvc;
using Jwc.Experiment.Xunit;
using Jwc.TfsBuilder.WebApplication.Infrastructure;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication
{
    public class FilterConfigTest
    {
        [Test]
        public void RegistersCorrectGlobalFilters(GlobalFilterCollection filters)
        {
            Assert.Empty(filters);

            FilterConfig.RegisterGlobalFilters(filters);

            Assert.Equal(
                new[] { typeof(NotifyErrorAttribute), typeof(HandleErrorAttribute) },
                filters.Select(x => x.Instance.GetType()));
        }

        [Test]
        public void RegistersCorrectNotifyErrorAttribute(
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