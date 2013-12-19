using System;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class EmailLoggerTest
    {
        [Spec]
        public void IsLogger(EmailLogger sut)
        {
            Assert.IsAssignableFrom<ILogger>(sut);
        }

        [Spec]
        public void LogWithNullSubjectThrows(EmailLogger sut, string body)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Log(null, body));

            Assert.Equal("subject", e.ParamName);
        }

        [Spec]
        public void LogWithNullBodyThrows(EmailLogger sut, string subject)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Log(subject, null));

            Assert.Equal("body", e.ParamName);
        }

        [Spec(Skip = "First, to run this test, set id and password in app.config and run this test explicitly.")]
        public void LogWithValidParametersSendsEmail(EmailLogger sut, string subject, string message)
        {
            sut.Log(subject, message);
        }
    }
}