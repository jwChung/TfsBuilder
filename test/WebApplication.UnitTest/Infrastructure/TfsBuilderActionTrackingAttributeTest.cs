using GoogleAnalyticsTracker.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class TfsBuilderActionTrackingAttributeTest
    {
        [Spec]
        public void IsActionTrackingAttribute(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] TfsBuilderActionTrackingAttribute sut)
        {
            Assert.IsAssignableFrom<ActionTrackingAttribute>(sut);
        }

        [Spec]
        public void GetsTrackingAccount(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] TfsBuilderActionTrackingAttribute sut)
        {
            var expected = AppSettings.Instance.GoogleAnalyticsTrackingId;

            var actual = sut.Tracker.TrackingAccount;

            Assert.Equal(expected, actual);
        }
    }
}