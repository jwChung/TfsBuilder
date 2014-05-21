using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Jwc.Experiment.Xunit;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class RequireAppHarborHttpsAttributeTest
    {
        [Test]
        public void IsRequireHttpsAttribute(RequireAppHarborHttpsAttribute sut)
        {
            Assert.IsAssignableFrom<RequireHttpsAttribute>(sut);
        }

        [Test]
        public void OnAuthorizationWithNullFilterContextThrows(
            RequireAppHarborHttpsAttribute sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.OnAuthorization(null));
            Assert.Equal("filterContext", e.ParamName);
        }

        [Test]
        public void OnAuthorizationWithSecureConnectionDoesNotThrow(
            RequireAppHarborHttpsAttribute sut,
            Mock<AuthorizationContext> mockFilterContext)
        {
            mockFilterContext.CallBase = false;
            mockFilterContext.SetupGet(x => x.HttpContext.Request.IsSecureConnection).Returns(true);

            Assert.DoesNotThrow(() => sut.OnAuthorization(mockFilterContext.Object));
        }

        [Test]
        [InlineData("https")]
        [InlineData("HTTPS")]
        [InlineData("Https")]
        public void OnAuthorizationWithXForwardedProtocolAsHttpsDoesNotThrow(
            string value,
            RequireAppHarborHttpsAttribute sut,
            Mock<AuthorizationContext> mockFilterContext)
        {
            mockFilterContext.CallBase = false;
            var nameValues = new NameValueCollection { { "X-Forwarded-Proto", value } };
            mockFilterContext.SetupGet(x => x.HttpContext.Request.Headers).Returns(nameValues);

            Assert.DoesNotThrow(() => sut.OnAuthorization(mockFilterContext.Object));
        }

        [Test]
        public void OnAuthorizationWithLocalRequestDoesNotThrow(
            RequireAppHarborHttpsAttribute sut,
            Mock<AuthorizationContext> mockFilterContext)
        {
            mockFilterContext.CallBase = false;
            mockFilterContext.SetupGet(x => x.HttpContext.Request.IsLocal).Returns(true);

            Assert.DoesNotThrow(() => sut.OnAuthorization(mockFilterContext.Object));
        }

        [Test]
        public void OnAuthorizationWithNonSSLRequestThrows(
            RequireAppHarborHttpsAttribute sut,
            Mock<AuthorizationContext> mockFilterContext)
        {
            mockFilterContext.CallBase = false;

            var e = Assert.Throws<InvalidOperationException>(() => sut.OnAuthorization(mockFilterContext.Object));
            Assert.Contains("The requested resource can only be accessed via SSL", e.Message);
        }
    }
}