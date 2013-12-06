﻿using System.Configuration;
using Jwc.AutoFixture.Xunit;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class AppSettingsTest
    {
        [Spec]
        public void InstanceIsSingleton()
        {
            var actual = AppSettings.Instance;

            Assert.NotNull(actual);
            Assert.Same(AppSettings.Instance, actual);
        }

        [Spec]
        public void InstanceHasGmailIdAsDynamic()
        {
            var expected = ConfigurationManager.AppSettings["GmailId"];

            var actual = AppSettings.Instance.GmailId;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void InstanceHasGoogleAnalyticsTrackingIdAsDynamic()
        {
            var expected = ConfigurationManager.AppSettings["GoogleAnalyticsTrackingId"];

            var actual = AppSettings.Instance.GoogleAnalyticsTrackingId;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void InstanceThrowsIfDynamicPropertyDoesNotExist()
        {
            var e = Assert.Throws<RuntimeBinderException>(() => AppSettings.Instance.InvalidProperty);
            Assert.Contains("The 'InvalidProperty' app-setting is not defined in the config file.", e.Message);
        }

        [Spec]
        public void InstanceDoesNotSupportSettingValue(string value)
        {
            var e = Assert.Throws<RuntimeBinderException>(
                () => AppSettings.Instance.GoogleAnalyticsTrackingId = value);

            Assert.Contains("AppSettings does not support settings a value.", e.Message);
        }
    }
}