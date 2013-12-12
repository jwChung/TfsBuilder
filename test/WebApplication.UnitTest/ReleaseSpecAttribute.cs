#if !RELEASE
using System;
#endif
using Jwc.AutoFixture.Xunit;

namespace Jwc.TfsBuilder.WebApplication
{
    public class ReleaseSpecAttribute : SpecAttribute
    {
#if !RELEASE
        public override string Skip
        {
            get
            {
                return "Only run on Release mode mainly because a test is a kind of integration-testing and very slow.";
            }
            set
            {
                throw new NotSupportedException();
            }
        }
#endif
    }
}