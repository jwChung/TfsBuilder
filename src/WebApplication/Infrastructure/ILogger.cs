namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="subject">A subject of the message.</param>
        /// <param name="body">A body of the message.</param>
        void Log(string subject, string body);
    }
}