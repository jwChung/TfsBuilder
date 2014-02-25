using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Models
{
    public class BuildParametersTest
    {
        [Spec]
        public void AccountHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Account");
        }

        [Spec]
        public void AccountIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.Account;
            Assert.Null(actual);
        }

        [Spec]
        public void AccountReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.Account = value;
            var actual = sut.Account;
            Assert.Equal(value, actual);
        }

        [Spec]
        public void TeamProjectHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("TeamProject");
        }

        [Spec]
        public void TeamProjectIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.TeamProject;

            Assert.Null(actual);
        }

        [Spec]
        public void TeamProjectReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.TeamProject = value;
            string actual = sut.TeamProject;
            Assert.Equal(value, actual);
        }

        [Spec]
        public void DefinitionNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("DefinitionName");
        }

        [Spec]
        public void DefinitionNameIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.DefinitionName;
            Assert.Null(actual);
        }

        [Spec]
        public void DefinitionNameReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.DefinitionName = value;
            string actual = sut.DefinitionName;
            Assert.Equal(value, actual);
        }

        [Spec]
        public void PayLoadHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("PayLoad");
        }

        [Spec]
        public void PayLoadIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.PayLoad;
            Assert.Null(actual);
        }

        [Spec]
        public void PayLoadReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.PayLoad = value;
            string actual = sut.PayLoad;
            Assert.Equal(value, actual);
        }

        [Spec]
        public void UserNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("UserName");
        }

        [Spec]
        public void UserNameIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.UserName;
            Assert.Null(actual);
        }

        [Spec]
        public void UserNameReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.UserName = value;
            string actual = sut.UserName;
            Assert.Equal(value, actual);
        }

        [Spec]
        public void PasswordHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Password");
        }

        [Spec]
        public void PasswordIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.Password;
            Assert.Null(actual);
        }

        [Spec]
        public void PasswordReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.Password = value;
            string actual = sut.Password;
            Assert.Equal(value, actual);
        }

        private static void AssertToHaveRequiredAttribute(string propertyName)
        {
            var sut = typeof(BuildParameters).GetProperty(propertyName);
            var actual = sut.GetCustomAttribute<RequiredAttribute>();
            Assert.NotNull(actual);
        }
    }
}