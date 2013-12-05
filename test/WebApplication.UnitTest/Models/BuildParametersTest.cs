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
        public void AccountIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut)
        {
            var actual = sut.Account;
            
            Assert.Null(actual);
        }

        [Spec]
        public void AccountSetsCorrectValue(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut,
            string value)
        {
            sut.Account = value;

            Assert.Equal(value, sut.Account);
        }

        [Spec]
        public void TeamProjectHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("TeamProject");
        }

        [Spec]
        public void TeamProjectIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut)
        {
            var actual = sut.TeamProject;

            Assert.Null(actual);
        }

        [Spec]
        public void TeamProjectSetsCorrectValue(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut,
            string value)
        {
            sut.TeamProject = value;

            Assert.Equal(value, sut.TeamProject);
        }

        [Spec]
        public void DefinitionNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("DefinitionName");
        }

        [Spec]
        public void DefinitionNameIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut)
        {
            var actual = sut.DefinitionName;

            Assert.Null(actual);
        }

        [Spec]
        public void DefinitionNameSetsCorrectValue(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut,
            string value)
        {
            sut.DefinitionName = value;

            Assert.Equal(value, sut.DefinitionName);
        }

        [Spec]
        public void PayLoadHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("PayLoad");
        }

        [Spec]
        public void PayLoadIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut)
        {
            var actual = sut.PayLoad;

            Assert.Null(actual);
        }

        [Spec]
        public void PayLoadSetsCorrectValue(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut,
            string value)
        {
            sut.PayLoad = value;

            Assert.Equal(value, sut.PayLoad);
        }

        [Spec]
        public void UserNameHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("UserName");
        }

        [Spec]
        public void UserNameIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut)
        {
            var actual = sut.UserName;

            Assert.Null(actual);
        }

        [Spec]
        public void UserNameSetsCorrectValue(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut,
            string value)
        {
            sut.UserName = value;

            Assert.Equal(value, sut.UserName);
        }

        [Spec]
        public void PasswordHasRequiredAttribute()
        {
            AssertToHaveRequiredAttribute("Password");
        }

        [Spec]
        public void PasswordIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut)
        {
            var actual = sut.Password;

            Assert.Null(actual);
        }

        [Spec]
        public void PasswordSetsCorrectValue(
            [Build(BuildFlags.NoAutoProperties)] BuildParameters sut,
            string value)
        {
            sut.Password = value;

            Assert.Equal(value, sut.Password);
        }

        private static void AssertToHaveRequiredAttribute(string propertyName)
        {
            var sut = typeof(BuildParameters).GetProperty(propertyName);

            var actual = sut.GetCustomAttribute<RequiredAttribute>();

            Assert.NotNull(actual);
        }
    }
}