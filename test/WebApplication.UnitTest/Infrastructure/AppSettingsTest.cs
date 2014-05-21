using System.Configuration;
using Jwc.Experiment.Xunit;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class AppSettingsTest
    {
        [Test]
        public void InstanceIsSingleton()
        {
            var actual = AppSettings.Instance;

            Assert.NotNull(actual);
            Assert.Same(AppSettings.Instance, actual);
        }

        [Test]
        public void InstanceHasGmailIdAsDynamic(string expected)
        {
            ConfigurationManager.AppSettings["GmailId"] = expected;
            var actual = AppSettings.Instance.GmailId;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void InstanceHasGoogleAnalyticsTrackingIdAsDynamic(string expected)
        {
            ConfigurationManager.AppSettings["GoogleAnalyticsTrackingId"] = expected;
            var actual = AppSettings.Instance.GoogleAnalyticsTrackingId;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void InstanceThrowsIfDynamicPropertyDoesNotExist()
        {
            var e = Assert.Throws<RuntimeBinderException>(() => AppSettings.Instance.InvalidProperty);
            Assert.Contains("The 'InvalidProperty' app-setting is not defined in the config file.", e.Message);
        }

        [Test]
        public void InstanceDoesNotSupportSettingValue(string value)
        {
            var e = Assert.Throws<RuntimeBinderException>(
                () => AppSettings.Instance.GoogleAnalyticsTrackingId = value);

            Assert.Contains("AppSettings does not support setting a value.", e.Message);
        }
    }
}