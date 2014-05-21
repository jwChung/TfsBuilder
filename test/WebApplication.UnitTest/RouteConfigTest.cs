using System;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using Jwc.Experiment.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication
{
    public class RouteConfigTest
    {
        [Test]
        public void RegistersCorrectBuildRoute(
            string s1,
            string s2,
            string s3,
            RouteCollection routes)
        {
            var url = string.Format("~/api/{0}/{1}/{2}", s1, s2, s3);
            var httpContext = new StubHttpContext(new StubHttpRequest(url));

            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            Assert.Equal("TfsBuilder", routeData.Values["controller"]);
            Assert.Equal("Build", routeData.Values["action"]);
            Assert.Equal(s1, routeData.Values["account"]);
            Assert.Equal(s2, routeData.Values["teamProject"]);
            Assert.Equal(s3, routeData.Values["definitionName"]);
        }

        [Test]
        [InlineData("~/")]
        [InlineData("~/Home")]
        [InlineData("~/Home/Index")]
        [InlineData("~/home/index")]
        public void RegistersHomeIndexRoute(
            string url,
            RouteCollection routes)
        {
            var httpContext = new StubHttpContext(new StubHttpRequest(url));

            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            var stringComparer = StringComparer.Create(CultureInfo.CurrentCulture, true);
            Assert.Equal("Home", (string)routeData.Values["controller"], stringComparer);
            Assert.Equal("Index", (string)routeData.Values["action"], stringComparer);
        }

        [Test]
        public void DoesNotRegisterAnyOtherRoute(
            string controller,
            string action,
            RouteCollection routes)
        {
            var url = string.Format("{0}/{1}", controller, action);
            var httpContext = new StubHttpContext(new StubHttpRequest(url));
            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            Assert.Null(routeData);
        }

        private class StubHttpContext : HttpContextBase
        {
            private readonly HttpRequestBase _request;

            public StubHttpContext(HttpRequestBase request)
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

        private class StubHttpRequest : HttpRequestBase
        {
            private readonly string _url;

            public StubHttpRequest(string url)
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