#if !Publish
using System;
#endif
using Jwc.AutoFixture.Xunit;

namespace Jwc.TfsBuilder.WebApplication
{
    public class PublishSpecAttribute : SpecAttribute
    {
#if !PUBLISH
        public override string Skip
        {
            get
            {
                return "Only run on Publish mode mainly because a test is slow or kind of integration-testing.";
            }
            set
            {
                throw new NotSupportedException();
            }
        }
#endif
    }
}