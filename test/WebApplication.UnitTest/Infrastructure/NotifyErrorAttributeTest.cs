using System;
using System.Reflection;
using System.Web.Mvc;
using Moq;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class NotifyErrorAttributeTest
    {
        [Theorem]
        public void IsFilterAttribute(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<FilterAttribute>(sut);
        }

        [Theorem]
        public void IsExceptionFilter(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<IExceptionFilter>(sut);
        }

        // [Theorem] TODO: skip
        //[InlineData(null)]
        //public void ConstructWithNullLoggerThrows(
        //    [Inject] ILogger logger,
        //    [Build] Lazy<NotifyErrorAttribute> sut)
        //{
        //    var e = Assert.Throws<TargetInvocationException>(() => sut.Value);
        //    var inner = Assert.IsType<ArgumentNullException>(e.InnerException);
        //    Assert.Equal("logger", inner.ParamName);
        //}

        [Theorem]
        public void GetsLogger(
            [Frozen] ILogger expected,
            [Greedy] NotifyErrorAttribute sut)
        {
            var actual = sut.Logger;

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void OnExceptionWithNullFilterContextThrows(NotifyErrorAttribute sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.OnException(null));
            Assert.Equal("filterContext", e.ParamName);
        }
        
        [Theorem]
        public void OnExceptionLogsAndHandlesException(
            Exception exception,
            [Frozen] ILogger logger,
            [Greedy] NotifyErrorAttribute sut)
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