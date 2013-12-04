using System;
using System.Collections.Generic;
using System.Reflection;
using Jwc.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class ExceptionSpecificationTest
    {
        [Spec]
        public void SutIsEquatableOfException(ExceptionSpecification sut)
        {
            Assert.IsAssignableFrom<IEquatable<Exception>>(sut);
        }

        [Spec]
        [InlineData(null)]
        public void CtorWithNullConditionThrows(
            [Inject] Func<Exception, bool> condition,
            [Build] Lazy<ExceptionSpecification> sut)
        {
            var e = Assert.Throws<TargetInvocationException>(() => sut.Value);
            var inner = Assert.IsType<ArgumentNullException>(e.InnerException);
            Assert.Equal("condition", inner.ParamName);
        }

        [Spec]
        public void ConditionIsCorrect(
            [Inject] Func<Exception, bool> expected,
            [Build] ExceptionSpecification sut)
        {
            var actual = sut.Condition;

            Assert.Equal(expected, actual);
        }

        private class EqualsDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                var e1 = new Exception();
                yield return new object[]
                {
                    e1,
                    false,
                    (Func<Exception, bool>)(e =>
                    {
                        Assert.Equal(e1, e);
                        return false;
                    })
                };

                var e2 = new Exception();
                yield return new object[]
                {
                    e2,
                    true,
                    (Func<Exception, bool>)(e =>
                    {
                        Assert.Equal(e2, e);
                        return true;
                    })
                };
            }
        }

        [Spec]
        [EqualsData]
        public void EqualsDelegatesToConditionPassedFromCtor(
            Exception exception,
            bool expected,
            [Inject] Func<Exception, bool> condition,
            [Build] ExceptionSpecification sut)
        {
            var actual = sut.Equals(exception);

            Assert.Equal(expected, actual);
        }
    }
}