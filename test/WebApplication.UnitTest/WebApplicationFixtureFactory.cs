using System.Reflection;
using Jwc.Experiment.AutoFixture;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Jwc.TfsBuilder.WebApplication
{
    public class WebApplicationFixtureFactory : TestFixtureFactory
    {
        protected override IFixture CreateFixture(MethodInfo testMethod)
        {
            return base.CreateFixture(testMethod)
                .Customize(new AutoMoqCustomization());
        }
    }
}