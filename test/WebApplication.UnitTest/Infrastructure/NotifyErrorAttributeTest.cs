using System;
using System.Reflection;
using System.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class NotifyErrorAttributeTest
    {
        [Spec]
        public void IsFilterAttribute(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<FilterAttribute>(sut);
        }

        [Spec]
        public void IsExceptionFilter(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<IExceptionFilter>(sut);
        }

        [Spec]
        [InlineData(null)]
        public void ConstructWithNullLoggerThrows(
            [Inject] ILogger logger,
            [Build] Lazy<NotifyErrorAttribute> sut)
        {
            var e = Assert.Throws<TargetInvocationException>(() => sut.Value);
            var inner = Assert.IsType<ArgumentNullException>(e.InnerException);
            Assert.Equal("logger", inner.ParamName);
        }

        [Spec]
        public void GetsLogger(
            [Inject] ILogger expected,
            [Build] NotifyErrorAttribute sut)
        {
            var actual = sut.Logger;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void OnExceptionWithNullFilterContextThrows(NotifyErrorAttribute sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.OnException(null));
            Assert.Equal("filterContext", e.ParamName);
        }
        
        [Spec]
        public void OnExceptionLogsAndHandlesException(
            Exception exception,
            [Inject] ILogger logger,
            [Build] NotifyErrorAttribute sut)
        {
            // Arrange
            var context = new ExceptionContext { Exception = exception };
            Assert.False(context.ExceptionHandled);

            // Act
            sut.OnException(context);

            // Assert
            Assert.True(context.ExceptionHandled);

            string subject = "TfsBuilder - " + context.Exception.GetType().Name;
            string body = context.Exception.ToString();
            Mock.Get(logger).Verify(x => x.Log(subject, body), Times.Once());

            var result = Assert.IsAssignableFrom<HttpStatusCodeResult>(context.Result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}