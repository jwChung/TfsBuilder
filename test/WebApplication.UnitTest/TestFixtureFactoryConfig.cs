using System.Reflection;
using Jwc.Experiment.AutoFixture;
using Jwc.Experiment.Xunit;
using Jwc.TfsBuilder.WebApplication;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

[assembly: AssemblyFixtureConfig(typeof(TestFixtureFactoryConfig))]

namespace Jwc.TfsBuilder.WebApplication
{
    public class TestFixtureFactoryConfig
    {
        public TestFixtureFactoryConfig()
        {
            DefaultFixtureFactory.SetCurrent(new WebApplicationFixtureFactory());
        }

        private class WebApplicationFixtureFactory : TestFixtureFactory
        {
            protected override IFixture CreateFixture(MethodInfo testMethod)
            {
                return base.CreateFixture(testMethod).Customize(new AutoMoqCustomization());
            }
        }
    }
}