using System.ComponentModel.DataAnnotations;

namespace Jwc.TfsBuilder.WebApplication.Models
{
    /// <summary>
    /// Represents build parameters.
    /// </summary>
    public class BuildParameters
    {
        /// <summary>
        /// Gets or sets a value indicating a account, which means a first part of url.
        /// (https://{Account}.visualstudio.com)
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// Gets or sets a name of team project.
        /// </summary>
        [Required]
        public string TeamProject { get; set; }

        /// <summary>
        /// Gets or sets a build definition name of <see cref="TeamProject"/>.
        /// </summary>
        [Required]
        public string DefinitionName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating a playload passed from github.
        /// </summary>
        [Required]
        public string PayLoad { get; set; }


        /// <summary>
        /// Gets or sets a value indicating a user name of TFS. This user name should be ALTERNATE AUTHENTICATION
        /// CREDENTIALS.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating a user password of TFS. This password should be ALTERNATE AUTHENTICATION
        /// CREDENTIALS.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}