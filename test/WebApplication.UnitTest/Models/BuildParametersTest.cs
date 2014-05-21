using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Jwc.Experiment.Xunit;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Models
{
    public class BuildParametersTest
    {
        [Test]
        public void AccountHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Account");
        }

        [Test]
        public void AccountIsNull(BuildParameters sut)
        {
            var actual = sut.Account;
            Assert.Null(actual);
        }

        [Test]
        public void AccountReturnsSetValue(
            BuildParameters sut,
            string value)
        {
            sut.Account = value;
            var actual = sut.Account;
            Assert.Equal(value, actual);
        }

        [Test]
        public void TeamProjectHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("TeamProject");
        }

        [Test]
        public void TeamProjectIsNull(BuildParameters sut)
        {
            var actual = sut.TeamProject;

            Assert.Null(actual);
        }

        [Test]
        public void TeamProjectReturnsSetValue(
            BuildParameters sut,
            string value)
        {
            sut.TeamProject = value;
            string actual = sut.TeamProject;
            Assert.Equal(value, actual);
        }

        [Test]
        public void DefinitionNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("DefinitionName");
        }

        [Test]
        public void DefinitionNameIsNull(BuildParameters sut)
        {
            var actual = sut.DefinitionName;
            Assert.Null(actual);
        }

        [Test]
        public void DefinitionNameReturnsSetValue(
            BuildParameters sut,
            string value)
        {
            sut.DefinitionName = value;
            string actual = sut.DefinitionName;
            Assert.Equal(value, actual);
        }

        [Test]
        public void PayLoadHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("PayLoad");
        }

        [Test]
        public void PayLoadIsNull(BuildParameters sut)
        {
            var actual = sut.PayLoad;
            Assert.Null(actual);
        }

        [Test]
        public void PayLoadReturnsSetValue(
            BuildParameters sut,
            string value)
        {
            sut.PayLoad = value;
            string actual = sut.PayLoad;
            Assert.Equal(value, actual);
        }

        [Test]
        public void UserNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("UserName");
        }

        [Test]
        public void UserNameIsNull(BuildParameters sut)
        {
            var actual = sut.UserName;
            Assert.Null(actual);
        }

        [Test]
        public void UserNameReturnsSetValue(
            BuildParameters sut,
            string value)
        {
            sut.UserName = value;
            string actual = sut.UserName;
            Assert.Equal(value, actual);
        }

        [Test]
        public void PasswordHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Password");
        }

        [Test]
        public void PasswordIsNull(BuildParameters sut)
        {
            var actual = sut.Password;
            Assert.Null(actual);
        }

        [Test]
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