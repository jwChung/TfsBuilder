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
        public void ExecuteWithNullParametersThrows(TfsBuildCommand sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Execute(null));
            Assert.Equal("parameters", e.ParamName);
        }

        [PublishSpec]
        public void ExecuteWithInvalidAccountThrows(TfsBuildCommand sut, string invalidAccount)
        {
            var parameters = CreateValidBuildParameters();
            parameters.Account = invalidAccount;

            var e = Assert.Throws<TfsBuildException>(() => sut.Execute(parameters));
            var inner = Assert.IsType<TeamFoundationServiceUnavailableException>(e.InnerException);
            Assert.Contains(
                "Unable to connect to this Team Foundation Server",
                inner.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly")]
        public void ExecuteWithInvalidTeamProjectThrows(TfsBuildCommand sut, string invalidTeamProject)
        {
            var parameters = CreateValidBuildParameters();
            parameters.TeamProject = invalidTeamProject;

            var e = Assert.Throws<TfsBuildException>(() => sut.Execute(parameters));
            var inner = Assert.IsType<ProjectDoesNotExistWithNameException>(e.InnerException);
            Assert.Contains(
                "The following project does not exist: " + invalidTeamProject,
                inner.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly")]
        public void ExecuteWithInvalidDefinitionNameThrows(TfsBuildCommand sut, string invalidDefinitionName)
        {
            var parameters = CreateValidBuildParameters();
            parameters.DefinitionName = invalidDefinitionName;

            var e = Assert.Throws<TfsBuildException>(() => sut.Execute(parameters));
            Assert.Contains(
                "No build definition was found for team project AutoFixture.Contrib with name "
                + invalidDefinitionName,
                e.Message);
        }

        [Spec(Skip = "To run this test, set build information below and run this test explicitly")]
        public void ExecuteWithValidParametersCreatesQueueBuild(TfsBuildCommand sut)
        {
            var parameters = CreateValidBuildParameters();

            sut.Execute(parameters);
        }

        private static BuildParameters CreateValidBuildParameters()
        {
            return new BuildParameters
            {
                Account = "*****",
                TeamProject = "AutoFixture.Contrib",
                DefinitionName = "*****",
                UserName = "*****",
                Password = "*****"
            };
        }
    }
}