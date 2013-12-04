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
    }
}