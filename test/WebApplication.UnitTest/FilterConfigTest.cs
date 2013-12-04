using System;
using System.Linq;
using System.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Jwc.TfsBuilder.WebApplication.Infrastructure;
using Xunit;
using Xunit.Extensions;

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
                new[] { typeof(NotifyErrorAttribute), typeof(HandleErrorAttribute) },
                filters.Select(x => x.Instance.GetType()));
        }

        [Spec]
        [InlineData(typeof(Exception), true)]
        [InlineData(typeof(InvalidOperationException), true)]
        [InlineData(typeof(ArgumentException), true)]
        [InlineData(typeof(ArgumentNullException), true)]
        [InlineData(typeof(TfsBuildException), true)]
        public void RegisterGlobalFiltersRegistersCorrectNotifyErrorAttribute(
            Type exceptionType,
            bool expected,
            GlobalFilterCollection filters)
        {
            // Arrange
            // Act
            FilterConfig.RegisterGlobalFilters(filters);

            // Assert
            var notifyErrorAttribute = filters.Select(f => f.Instance).OfType<NotifyErrorAttribute>().Single();
            Assert.IsType<EmailLogger>(notifyErrorAttribute.Logger);

            var condition = Assert.IsType<ExceptionSpecification>(notifyErrorAttribute.Condition);
            var result = condition.Equals((Exception)Activator.CreateInstance(exceptionType));
            Assert.Equal(expected, result);
        }
    }
}