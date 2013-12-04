using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Jwc.TfsBuilder.WebApplication.Models;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents a build command.
    /// </summary>
    public class TfsBuildCommand
    {
        private BuildParameters _parameters;

        /// <summary>
        /// Execute a build command.
        /// </summary>
        /// <param name="parameters">The build parameters.</param>
        public virtual void Execute(BuildParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            _parameters = parameters;

            using (TfsConnection teamProjects = CreateTfsTeamProjectCollection())
            {
                Authenticate(teamProjects);
                QueueBuild(teamProjects);
            }
        }

        private TfsConnection CreateTfsTeamProjectCollection()
        {
            var uri = new Uri(string.Format(
                CultureInfo.CurrentCulture,
                "https://{0}.visualstudio.com/DefaultCollection",
                _parameters.Account));

            var tfsCred = new TfsClientCredentials(
                new BasicAuthCredential(
                    new NetworkCredential(_parameters.UserName, _parameters.Password)))
            {
                AllowInteractive = false
            };

            return new TfsTeamProjectCollection(uri, tfsCred);
        }

        private static void Authenticate(TfsConnection teamProjects)
        {
            try
            {
                teamProjects.Authenticate();
            }
            catch (TeamFoundationServiceUnavailableException exception)
            {
                throw new TfsBuildException(
                    "Check out the inner exception",
                    exception);
            }
        }

        private void QueueBuild(TfsConnection teamProjects)
        {       
            var buildServer = teamProjects.GetService<IBuildServer>();
            buildServer.QueueBuild(GetBuildDefinition(buildServer));
        }

        private IBuildDefinition GetBuildDefinition(IBuildServer buildServer)
        {
            try
            {
                return GetBuildDefinitionImpl(buildServer);
            }
            catch (ProjectDoesNotExistWithNameException exception)
            {
                throw new TfsBuildException(
                    "Check out the inner exception",
                    exception);
            }
        }

        private IBuildDefinition GetBuildDefinitionImpl(IBuildServer buildServer)
        {
            IBuildDefinition buildDefinition = buildServer
                .QueryBuildDefinitions(_parameters.TeamProject)
                .SingleOrDefault(x => x.Name == _parameters.DefinitionName);

            if (buildDefinition == null)
            {
                throw new TfsBuildException(string.Format(
                    CultureInfo.CurrentCulture,
                    "No build definition was found for team project AutoFixture.Contrib with name {0}.",
                    _parameters.DefinitionName));
            }

            return buildDefinition;
        }
    }
}