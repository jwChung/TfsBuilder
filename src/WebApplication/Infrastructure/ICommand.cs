namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    /// <typeparam name="T">A type of a comand parameter.</typeparam>
    public interface ICommand<in T>
    {
        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="parameter">A command paramter.</param>
        void Execute(T parameter);
    }
}