using GoogleAnalyticsTracker.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class TfsBuilderActionTrackingAttributeTest
    {
        [Spec]
        public void SutIsActionTrackingAttribute(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderActionTrackingAttribute sut)
        {
            Assert.IsAssignableFrom<ActionTrackingAttribute>(sut);
        }

        [Spec]
        public void TrackingAccountIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderActionTrackingAttribute sut)
        {
            var expected = AppSettings.Instance.GoogleAnalyticsTrackingId;

            var actual = sut.Tracker.TrackingAccount;

            Assert.Equal(expected, actual);
        }
    }
}