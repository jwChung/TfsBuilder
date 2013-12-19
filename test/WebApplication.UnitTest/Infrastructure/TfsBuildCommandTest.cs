using System;
using Jwc.AutoFixture.Xunit;
using Jwc.TfsBuilder.WebApplication.Models;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Framework.Client;
using Xunit;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    public class TfsBuildCommandTest
    {
        [Spec]
        public void IsCommandOfBuildParameters(TfsBuildCommand sut)
        {
            Assert.IsAssignableFrom<ICommand<BuildParameters>>(sut);
        }

        [Spec]
        public void ExecuteWithNullParametersThrows(TfsBuildCommand sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Execute(null));
            Assert.Equal("parameters", e.ParamName);
        }

        [ReleaseSpec]
        public void ExecuteWithInvalidAccountThrows(TfsBuildCommand sut, string invalidAccount)
        {
            var parameters = CreateValidBuildParameters();
            parameters.Account = invalidAccount;

            var e = Assert.Throws<TeamFoundationServiceUnavailableException>(() => sut.Execute(parameters));
            Assert.Contains(
                "Unable to connect to this Team Foundation Server",
                e.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly.")]
        public void ExecuteWithInvalidTeamProjectThrows(TfsBuildCommand sut, string invalidTeamProject)
        {
            var parameters = CreateValidBuildParameters();
            parameters.TeamProject = invalidTeamProject;

            var e = Assert.Throws<ProjectDoesNotExistWithNameException>(() => sut.Execute(parameters));
            Assert.Contains(
                "The following project does not exist: " + invalidTeamProject,
                e.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly.")]
        public void ExecuteWithInvalidDefinitionNameThrows(TfsBuildCommand sut, string invalidDefinitionName)
        {
            var parameters = CreateValidBuildParameters();
            parameters.DefinitionName = invalidDefinitionName;

            var e = Assert.Throws<TfsBuildException>(() => sut.Execute(parameters));
            Assert.Contains(
                string.Format(
                    "No build definition was found for team project {0} with name {1}",
                    parameters.TeamProject,
                    invalidDefinitionName),
                e.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly.")]
        public void ExecuteWithInvalidUserNameThrows(TfsBuildCommand sut, string invalidUserName)
        {
            var parameters = CreateValidBuildParameters();
            parameters.UserName = invalidUserName;

            var e = Assert.Throws<TeamFoundationServerUnauthorizedException>(() => sut.Execute(parameters));
            Assert.Contains(
                "You are not authorized",
                e.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly.")]
        public void ExecuteWithInvalidPasswordThrows(TfsBuildCommand sut, string invalidPassword)
        {
            var parameters = CreateValidBuildParameters();
            parameters.Password = invalidPassword;

            var e = Assert.Throws<TeamFoundationServerUnauthorizedException>(() => sut.Execute(parameters));
            Assert.Contains(
                "You are not authorized",
                e.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly.")]
        public void ExecuteQueuesBuildProcess(TfsBuildCommand sut)
        {
            var parameters = CreateValidBuildParameters();

            sut.Execute(parameters);
        }

        private static BuildParameters CreateValidBuildParameters()
        {
            return new BuildParameters
            {
                Account = "*****",
                TeamProject = "*****",
                DefinitionName = "*****",
                UserName = "*****",
                Password = "*****"
            };
        }
    }
}