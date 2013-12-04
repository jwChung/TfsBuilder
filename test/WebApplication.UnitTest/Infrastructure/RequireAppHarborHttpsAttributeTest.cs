using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class RequireAppHarborHttpsAttributeTest
    {
        [Spec]
        public void SutIsRequireHttpsAttribute(RequireAppHarborHttpsAttribute sut)
        {
            Assert.IsAssignableFrom<RequireHttpsAttribute>(sut);
        }

        [Spec]
        public void OnAuthorizationWithNullFilterContextThrows(
            RequireAppHarborHttpsAttribute sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.OnAuthorization(null));
            Assert.Equal("filterContext", e.ParamName);
        }

        [Spec]
        public void OnAuthorizationDoesNotThrowIfIsSecureConnectionIsTrue(
            RequireAppHarborHttpsAttribute sut,
            [Build(BuildFlags.ForceMocked)] AuthorizationContext filterContext)
        {
            Mock.Get(filterContext).CallBase = false;
            Mock.Get(filterContext).SetupGet(x => x.HttpContext.Request.IsSecureConnection).Returns(true);

            Assert.DoesNotThrow(() => sut.OnAuthorization(filterContext));
        }

        [Spec]
        [InlineData("https")]
        [InlineData("HTTPS")]
        [InlineData("Https")]
        public void OnAuthorizationDoesNotThrowIfXForwardedProtocolIsHttps(
            string value,
            RequireAppHarborHttpsAttribute sut,
            [Build(BuildFlags.ForceMocked)] AuthorizationContext filterContext)
        {
            Mock.Get(filterContext).CallBase = false;
            var nameValues = new NameValueCollection { { "X-Forwarded-Proto", value } };
            Mock.Get(filterContext).SetupGet(x => x.HttpContext.Request.Headers).Returns(nameValues);

            Assert.DoesNotThrow(() => sut.OnAuthorization(filterContext));
        }

        [Spec]
        public void OnAuthorizationDoesNotThrowIfRequestIsLocal(
            RequireAppHarborHttpsAttribute sut,
            [Build(BuildFlags.ForceMocked)] AuthorizationContext filterContext)
        {
            Mock.Get(filterContext).CallBase = false;
            Mock.Get(filterContext).SetupGet(x => x.HttpContext.Request.IsLocal).Returns(true);

            Assert.DoesNotThrow(() => sut.OnAuthorization(filterContext));
        }

        [Spec]
        public void OnAuthorizationThrowsIfRequestIsNotSSL(
            RequireAppHarborHttpsAttribute sut,
            [Build(BuildFlags.ForceMocked)] AuthorizationContext filterContext)
        {
            Mock.Get(filterContext).CallBase = false;

            var e = Assert.Throws<InvalidOperationException>(() => sut.OnAuthorization(filterContext));
            Assert.Contains("The requested resource can only be accessed via SSL", e.Message);
        }
    }
}