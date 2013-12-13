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
    [TfsBuilderActionTracking]
    public class TfsBuilderController : Controller
    {
        private readonly ICommand<BuildParameters> _buildCommand;

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
        public TfsBuilderController(ICommand<BuildParameters> buildCommand)
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
        public ICommand<BuildParameters> BuildCommand
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

            if (!TriggersBuild(parameters.PayLoad))
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

        private static bool TriggersBuild(string payload)
        {
            if (payload == "dummy")
            {
                return true;
            }

            return !IsTagging(payload);
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
            catch (TeamFoundationServerUnauthorizedException exception)
            {
                return exception.Message;
            }
            catch (TfsBuildException exception)
            {
                return exception.Message;
            }
            catch (UriFormatException exception)
            {
                return exception.Message;
            }

            return "Just have queued a new build process.";
        }

        private static bool IsTagging(string payload)
        {
            dynamic payloadJson = System.Web.Helpers.Json.Decode(payload);
            const string blank = "0000000000000000000000000000000000000000";
            return payloadJson.commits.Length == 0 && (payloadJson.after == blank || payloadJson.before == blank);
        }
    }
}
