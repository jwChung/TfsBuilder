using System;
using System.Reflection;
using System.Web.Mvc;
using Jwc.Experiment.AutoFixture;
using Jwc.Experiment.Xunit;
using Moq;
using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class NotifyErrorAttributeTest
    {
        [Test]
        public void IsFilterAttribute(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<FilterAttribute>(sut);
        }

        [Test]
        public void IsExceptionFilter(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<IExceptionFilter>(sut);
        }

        [Test]
        public void ConstructWithNullLoggerThrows(
            IFixture fixture)
        {
            fixture.Inject<ILogger>(null);
            var e = Assert.Throws<TargetInvocationException>(() => fixture.Create<NotifyErrorAttribute>());
            Assert.IsType<ArgumentNullException>(e.InnerException);
        }

        [Test]
        public void GetsLogger(
            [Frozen] ILogger expected,
            [Greedy] NotifyErrorAttribute sut)
        {
            var actual = sut.Logger;

            Assert.Equal(expected, actual);
        }

        [Test]
        public void OnExceptionWithNullFilterContextThrows(NotifyErrorAttribute sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.OnException(null));
            Assert.Equal("filterContext", e.ParamName);
        }
        
        [Test]
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