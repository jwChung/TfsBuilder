using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Models
{
    public class BuildParametersTest
    {
        [Theorem]
        public void AccountHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Account");
        }

        [Theorem]
        public void AccountIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.Account;
            Assert.Null(actual);
        }

        [Theorem]
        public void AccountReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.Account = value;
            var actual = sut.Account;
            Assert.Equal(value, actual);
        }

        [Theorem]
        public void TeamProjectHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("TeamProject");
        }

        [Theorem]
        public void TeamProjectIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.TeamProject;

            Assert.Null(actual);
        }

        [Theorem]
        public void TeamProjectReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.TeamProject = value;
            string actual = sut.TeamProject;
            Assert.Equal(value, actual);
        }

        [Theorem]
        public void DefinitionNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("DefinitionName");
        }

        [Theorem]
        public void DefinitionNameIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.DefinitionName;
            Assert.Null(actual);
        }

        [Theorem]
        public void DefinitionNameReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.DefinitionName = value;
            string actual = sut.DefinitionName;
            Assert.Equal(value, actual);
        }

        [Theorem]
        public void PayLoadHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("PayLoad");
        }

        [Theorem]
        public void PayLoadIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.PayLoad;
            Assert.Null(actual);
        }

        [Theorem]
        public void PayLoadReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.PayLoad = value;
            string actual = sut.PayLoad;
            Assert.Equal(value, actual);
        }

        [Theorem]
        public void UserNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("UserName");
        }

        [Theorem]
        public void UserNameIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.UserName;
            Assert.Null(actual);
        }

        [Theorem]
        public void UserNameReturnsSetValue(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut,
            string value)
        {
            sut.UserName = value;
            string actual = sut.UserName;
            Assert.Equal(value, actual);
        }

        [Theorem]
        public void PasswordHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Password");
        }

        [Theorem]
        public void PasswordIsNull(
            [Build(BuildOptions.Default & ~BuildOptions.AutoProperties)] BuildParameters sut)
        {
            var actual = sut.Password;
            Assert.Null(actual);
        }

        [Theorem]
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