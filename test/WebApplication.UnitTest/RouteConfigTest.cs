using System.Web;
using System.Web.Routing;
using Jwc.AutoFixture.Xunit;
using Moq;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication
{
    public class RouteConfigTest
    {
        [Spec]
        public void RegisterRoutesRegistersCorrectBuildRoute(
            string s1,
            string s2,
            string s3,
            HttpContextBase httpContext,
            RouteCollection routes)
        {
            var url = string.Format("~/api/{0}/{1}/{2}", s1, s2, s3);
            Mock.Get(httpContext).Setup(x => x.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            Mock.Get(httpContext).Setup(x => x.Request.PathInfo).Returns(string.Empty);

            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(httpContext);
            Assert.Equal("TfsBuilder", routeData.Values["controller"]);
            Assert.Equal("Build", routeData.Values["action"]);
            Assert.Equal(s1, routeData.Values["account"]);
            Assert.Equal(s2, routeData.Values["teamProject"]);
            Assert.Equal(s3, routeData.Values["definitionName"]);
        }
    }
}