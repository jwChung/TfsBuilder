using System;
using System.Runtime.Serialization;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents an exception thrown from TFS build process.
    /// </summary>
    [Serializable]
    public class TfsBuildException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuildException"/> class.
        /// </summary>
        public TfsBuildException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuildException"/> class.
        /// </summary>
        public TfsBuildException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuildException"/> class.
        /// </summary>
        public TfsBuildException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBuildException"/> class.
        /// </summary>
        protected TfsBuildException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}