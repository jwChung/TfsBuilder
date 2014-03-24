using System.IO;
using System.Web;
using System.Web.Mvc;
using MarkdownSharp;
using Moq;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Controllers
{
    public class HomeControllerTest
    {
        [Theorem]
        public void IsController(HomeController sut)
        {
            Assert.IsAssignableFrom<Controller>(sut);
        }

        [Theorem]
        public void IndexReturnsCorrectViewResult(
            HttpContextBase httpContext,
            ControllerContext controllerContext,
            HomeController sut)
        {
            // Arrange
            controllerContext.HttpContext = httpContext;
            sut.ControllerContext = controllerContext;
            
            const string readmeFilePath = "README.md";
            Mock.Get(httpContext).Setup(x => x.Server.MapPath("~/bin/README.md")).Returns(readmeFilePath);

            string markdown;
            using (var reader = new StreamReader(readmeFilePath))
            {
                markdown = reader.ReadToEnd();
            }

            var html = new Markdown().Transform(markdown);

            // Act
            var actual = sut.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actual);
            Assert.Empty(viewResult.ViewName);
            Assert.Equal(html, viewResult.Model);
        }
    }
}