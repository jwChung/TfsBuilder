using System;
using System.Web.Mvc;
using GoogleAnalyticsTracker.Web.Mvc;
using Moq;
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

        [Theorem]
        public void BuildCurrentActionUrlReturnsUrlWithout(
            TfsBuilderActionTrackingAttribute sut,
            Mock<ActionExecutingContext> mockContext)
        {
            mockContext.CallBase = false;
            var request = mockContext.Object.RequestContext.HttpContext.Request;
            var uri = new Uri("http://abc.com/abc/def?a=1&b=2");
            Mock.Get(request).SetupGet(x => x.Url).Returns(uri);

            var actual = sut.BuildCurrentActionUrl(mockContext.Object);

            Assert.Equal("/abc/def", actual);
        }
    }
}