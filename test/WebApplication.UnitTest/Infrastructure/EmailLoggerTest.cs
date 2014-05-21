using System;
using Jwc.Experiment.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class EmailLoggerTest
    {
        [Test]
        public void IsLogger(EmailLogger sut)
        {
            Assert.IsAssignableFrom<ILogger>(sut);
        }

        [Test]
        public void LogWithNullSubjectThrows(EmailLogger sut, string body)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Log(null, body));

            Assert.Equal("subject", e.ParamName);
        }

        [Test]
        public void LogWithNullBodyThrows(EmailLogger sut, string subject)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Log(subject, null));

            Assert.Equal("body", e.ParamName);
        }

        [Test(Skip = "First, to run this test, set id and password in app.config and run this test explicitly.")]
        public void LogWithValidParametersSendsEmail(EmailLogger sut, string subject, string message)
        {
            sut.Log(subject, message);
        }
    }
}