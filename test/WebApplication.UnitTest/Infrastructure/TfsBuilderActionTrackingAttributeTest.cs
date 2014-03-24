using System;
using GoogleAnalyticsTracker.Web.Mvc;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class TfsBuilderActionTrackingAttributeTest
    {
        [Theorem]
        public void IsActionTrackingAttribute(
            [NoAutoProperties] TfsBuilderActionTrackingAttribute sut)
        {
            Assert.IsAssignableFrom<ActionTrackingAttribute>(sut);
        }

        [Theorem]
        public void GetsTrackingAccount(
            [NoAutoProperties] TfsBuilderActionTrackingAttribute sut)
        {
            var expected = AppSettings.Instance.GoogleAnalyticsTrackingId;

            var actual = sut.Tracker.TrackingAccount;

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void BuildCurrentActionUrlWithNullContextThrows(
            [NoAutoProperties] TfsBuilderActionTrackingAttribute sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.BuildCurrentActionUrl(null));
        }
    }
}