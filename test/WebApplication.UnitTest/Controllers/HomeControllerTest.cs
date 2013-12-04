using System.Web.Mvc;
using Jwc.AutoFixture.Xunit;
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
            [Build(BuildFlags.NoAutoProperties)] HomeController sut)
        {
            var actual = sut.Index();

            var viewResult = Assert.IsAssignableFrom<ViewResult>(actual);
            Assert.Equal("Index", viewResult.ViewName);
        }
    }
}