using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Jwc.TfsBuilder.WebApplication.Models;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents a build command.
    /// </summary>
    public class TfsBuildCommand : ICommand<BuildParameters>
    {
        private BuildParameters _parameters;

        /// <summary>
        /// Execute a build command.
        /// </summary>
        /// <param name="buildParameters">The build parameters.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#")]
        public void Execute(BuildParameters buildParameters)
        {
            if (buildParameters == null)
            {
                throw new ArgumentNullException("buildParameters");
            }

            _parameters = buildParameters;

            using (TfsConnection teamProjects = CreateTfsTeamProjectCollection())
            {
                teamProjects.Authenticate();
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

        private void QueueBuild(TfsConnection teamProjects)
        {
            var buildServer = teamProjects.GetService<IBuildServer>();
            buildServer.QueueBuild(GetBuildDefinition(buildServer));
        }

        private IBuildDefinition GetBuildDefinition(IBuildServer buildServer)
        {
            IBuildDefinition buildDefinition = buildServer
                .QueryBuildDefinitions(_parameters.TeamProject)
                .SingleOrDefault(x => x.Name == _parameters.DefinitionName);

            if (buildDefinition == null)
            {
                throw new TfsBuildException(string.Format(
                    CultureInfo.CurrentCulture,
                    "No build definition was found for team project {0} with name {1}.",
                    _parameters.TeamProject,
                    _parameters.DefinitionName));
            }

            return buildDefinition;
        }
    }
}