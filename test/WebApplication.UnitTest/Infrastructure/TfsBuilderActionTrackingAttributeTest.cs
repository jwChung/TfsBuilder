using System.Configuration;
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
            var expected = ConfigurationManager.AppSettings["GoogleAnalyticsTrackingId"];

            var actual = sut.Tracker.TrackingAccount;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void TrackingDomainIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderActionTrackingAttribute sut)
        {
            var expected = ConfigurationManager.AppSettings["GoogleAnalyticsTrackingDomain"];

            var actual = sut.Tracker.TrackingDomain;

            Assert.Equal(expected, actual);
        }
    }
}