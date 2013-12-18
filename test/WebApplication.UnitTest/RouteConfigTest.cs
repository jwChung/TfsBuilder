using System;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using Jwc.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication
{
    public class RouteConfigTest
    {
        [Spec]
        public void RegisterRoutesRegistersCorrectBuildRoute(
            string s1,
            string s2,
            string s3,
            RouteCollection routes)
        {
            var url = string.Format("~/api/{0}/{1}/{2}", s1, s2, s3);
            var httpContext = new FakeHttpContext(new FakeHttpRequest(url));

            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            Assert.Equal("TfsBuilder", routeData.Values["controller"]);
            Assert.Equal("Build", routeData.Values["action"]);
            Assert.Equal(s1, routeData.Values["account"]);
            Assert.Equal(s2, routeData.Values["teamProject"]);
            Assert.Equal(s3, routeData.Values["definitionName"]);
        }

        [Spec]
        [InlineData("~/")]
        [InlineData("~/Home")]
        [InlineData("~/Home/Index")]
        [InlineData("~/home/index")]
        public void RegisterRoutesRegistersHomeIndex(
            string url,
            RouteCollection routes)
        {
            var httpContext = new FakeHttpContext(new FakeHttpRequest(url));

            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            var stringComparer = StringComparer.Create(CultureInfo.CurrentCulture, true);
            Assert.Equal("Home", (string)routeData.Values["controller"], stringComparer);
            Assert.Equal("Index", (string)routeData.Values["action"], stringComparer);
        }

        [Spec]
        public void RegisterRoutesDoesNotRegistersOtherControllerAndAction(
            string controller,
            string action,
            RouteCollection routes)
        {
            var url = string.Format("{0}/{1}", controller, action);
            var httpContext = new FakeHttpContext(new FakeHttpRequest(url));
            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            Assert.Null(routeData);
        }

        private class FakeHttpContext : HttpContextBase
        {
            private readonly HttpRequestBase _request;

            public FakeHttpContext(HttpRequestBase request)
            {
                _request = request;
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return _request;
                }
            }
        }

        private class FakeHttpRequest : HttpRequestBase
        {
            private readonly string _url;

            public FakeHttpRequest(string url)
            {
                _url = url;
            }

            public override string AppRelativeCurrentExecutionFilePath
            {
                get
                {
                    return _url;
                }
            }

            public override string PathInfo
            {
                get
                {
                    return string.Empty;
                }
            }
        }
    }
}