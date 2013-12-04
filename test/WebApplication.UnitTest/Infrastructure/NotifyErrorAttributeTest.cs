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
        public void SutIsFilterAttribute(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<FilterAttribute>(sut);
        }

        [Spec]
        public void SutIsExceptionFilter(NotifyErrorAttribute sut)
        {
            Assert.IsAssignableFrom<IExceptionFilter>(sut);
        }

        [Spec]
        [InlineData(null)]
        public void CtorWithNullLoggerThrows(
            [Inject] EmailLogger logger,
            [Build] Lazy<NotifyErrorAttribute> sut)
        {
            var e = Assert.Throws<TargetInvocationException>(() => sut.Value);
            var inner = Assert.IsType<ArgumentNullException>(e.InnerException);
            Assert.Equal("logger", inner.ParamName);
        }

        [Spec]
        [InlineData(null)]
        public void CtorWithNullExcludeExceptionThrows(
            [Inject] IEquatable<Exception> condition,
            [Build] Lazy<NotifyErrorAttribute> sut)
        {
            var e = Assert.Throws<TargetInvocationException>(() => sut.Value);
            var inner = Assert.IsType<ArgumentNullException>(e.InnerException);
            Assert.Equal("condition", inner.ParamName);
        }

        [Spec]
        public void LoggerIsCorrect(
            [Inject] EmailLogger expected,
            [Build] NotifyErrorAttribute sut)
        {
            var actual = sut.Logger;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ConditionIsCorrect(
            [Inject] IEquatable<Exception> expected,
            [Build] NotifyErrorAttribute sut)
        {
            var actual = sut.Condition;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void OnExceptionWithNullFilterContextThrows(NotifyErrorAttribute sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.OnException(null));
            Assert.Equal("filterContext", e.ParamName);
        }
        
        [Spec]
        public void OnExceptionLogsMessageAndHandlesExceptionIfExceptionSatisfiesCondition(
            Exception exception,
            [Inject][Build(BuildFlags.ForceMocked)] EmailLogger logger,
            [Inject] IEquatable<Exception> condition,
            [Build] NotifyErrorAttribute sut)
        {
            // Arrange
            Mock.Get(logger).CallBase = false;

            Mock.Get(condition).Setup(x => x.Equals(exception)).Returns(true);

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

        [Spec]
        public void OnExceptionDoesNotLogMessageAndDoesNotHandleExceptionIfExceptionDoesNotSatisfyCondition(
            Exception exception,
            [Inject][Build(BuildFlags.ForceMocked)] EmailLogger logger,
            [Inject] IEquatable<Exception> excludeException,
            [Build] NotifyErrorAttribute sut)
        {
            // Arrange
            Mock.Get(logger).CallBase = false;

            Mock.Get(excludeException).Setup(x => x.Equals(exception)).Returns(false);

            var context = new ExceptionContext { Exception = exception };
            Assert.False(context.ExceptionHandled);

            // Act
            sut.OnException(context);

            // Assert
            Assert.True(context.ExceptionHandled);

            Mock.Get(logger).Verify(x => x.Log(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

            var result = Assert.IsAssignableFrom<HttpStatusCodeResult>(context.Result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}