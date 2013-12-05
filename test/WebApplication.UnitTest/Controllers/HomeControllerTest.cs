using System.IO;
using System.Web;
using System.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using MarkdownSharp;
using Moq;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Controllers
{
    public class HomeControllerTest
    {
        [Spec]
        public void SutIsController(
            [Build(BuildFlags.NoAutoProperties)] HomeController sut)
        {
            Assert.IsAssignableFrom<Controller>(sut);
        }

        [Spec]
        public void IndexReturnsCorrectViewResult(
            HttpContextBase httpContext,
            [Build(BuildFlags.NoAutoProperties)] ControllerContext controllerContext,
            [Build(BuildFlags.NoAutoProperties)] HomeController sut)
        {
            // Arrange
            controllerContext.HttpContext = httpContext;
            sut.ControllerContext = controllerContext;
            
            const string readmeFilePath = @"..\..\..\..\README.md";
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
            Assert.Equal(html, viewResult.ViewBag.Body);
        }
    }
}