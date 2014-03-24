using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Ploeh.AutoFixture.Xunit;
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
        public void AccountIsNull(BuildParameters sut)
        {
            var actual = sut.Account;
            Assert.Null(actual);
        }

        [Theorem]
        public void AccountReturnsSetValue(
            BuildParameters sut,
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
        public void TeamProjectIsNull(BuildParameters sut)
        {
            var actual = sut.TeamProject;

            Assert.Null(actual);
        }

        [Theorem]
        public void TeamProjectReturnsSetValue(
            BuildParameters sut,
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
        public void DefinitionNameIsNull(BuildParameters sut)
        {
            var actual = sut.DefinitionName;
            Assert.Null(actual);
        }

        [Theorem]
        public void DefinitionNameReturnsSetValue(
            BuildParameters sut,
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
        public void PayLoadIsNull(BuildParameters sut)
        {
            var actual = sut.PayLoad;
            Assert.Null(actual);
        }

        [Theorem]
        public void PayLoadReturnsSetValue(
            BuildParameters sut,
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
        public void UserNameIsNull(BuildParameters sut)
        {
            var actual = sut.UserName;
            Assert.Null(actual);
        }

        [Theorem]
        public void UserNameReturnsSetValue(
            BuildParameters sut,
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
        public void PasswordIsNull(BuildParameters sut)
        {
            var actual = sut.Password;
            Assert.Null(actual);
        }

        [Theorem]
        public void PasswordReturnsSetValue(
            BuildParameters sut,
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