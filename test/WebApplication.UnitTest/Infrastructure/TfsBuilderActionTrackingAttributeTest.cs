using System;
using GoogleAnalyticsTracker.Web.Mvc;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class TfsBuilderActionTrackingAttributeTest
    {
        [Theorem]
        public void IsActionTrackingAttribute(TfsBuilderActionTrackingAttribute sut)
        {
            Assert.IsAssignableFrom<ActionTrackingAttribute>(sut);
        }

        [Theorem]
        public void GetsTrackingAccount(TfsBuilderActionTrackingAttribute sut)
        {
            var expected = AppSettings.Instance.GoogleAnalyticsTrackingId;

            var actual = sut.Tracker.TrackingAccount;

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void BuildCurrentActionUrlWithNullContextThrows(TfsBuilderActionTrackingAttribute sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.BuildCurrentActionUrl(null));
        }
    }
}