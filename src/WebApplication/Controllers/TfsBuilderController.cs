using System;
using System.Linq;
using System.Web.Mvc;
using Jwc.TfsBuilder.WebApplication.Infrastructure;
using Jwc.TfsBuilder.WebApplication.Models;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Framework.Client;

namespace Jwc.TfsBuilder.WebApplication.Controllers
{
    /// <summary>
    /// Represents TFS build controller.
    /// </summary>
    public class TfsBuilderController : Controller
    {
        private readonly TfsBuildCommand _buildCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuilderController"/>.
        /// </summary>
        public TfsBuilderController()
            : this(new TfsBuildCommand())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuilderController"/> with a build command.
        /// </summary>
        /// <param name="buildCommand">Provides a command to build.</param>
        public TfsBuilderController(TfsBuildCommand buildCommand)
        {
            if (buildCommand == null)
            {
                throw new ArgumentNullException("buildCommand");
            }

            _buildCommand = buildCommand;
        }

        /// <summary>
        /// Gets a value indicating the build command.
        /// </summary>
        public TfsBuildCommand BuildCommand
        {
            get
            {
                return _buildCommand;
            }
        }

        /// <summary>
        /// Adds a build queue to TFS.
        /// </summary>
        /// <param name="parameters">Information about a build queue of TFS.</param>
        /// <returns>An action result.</returns>
        [HttpPost]
        [ValidateInput(false)]
        [RequireAppHarborHttps]
        public ActionResult Build(BuildParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (!ModelState.IsValid)
            {
                return Content(GetInvalidModelStateMessage());
            }

            if (!HasCommits(parameters.PayLoad))
            {
                return Content("There are no commits to queue a build process.");
            }

            return Content(GetBuildResult(parameters));
        }

        private string GetInvalidModelStateMessage()
        {
            var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            return string.Join(Environment.NewLine, errorMessages);
        }

        private static bool HasCommits(string payload)
        {
            return !payload.Contains("\"commits\":[]");
        }

        private string GetBuildResult(BuildParameters parameters)
        {
            try
            {
                BuildCommand.Execute(parameters);
            }
            catch (TeamFoundationServiceUnavailableException exception)
            {
                return exception.Message;
            }
            catch (ProjectDoesNotExistWithNameException exception)
            {
                return exception.Message;
            }
            catch (TfsBuildException exception)
            {
                return exception.Message;
            }

            return "Just have queued a build process.";
        }
    }
}
