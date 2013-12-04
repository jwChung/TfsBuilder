using System;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents a delegate condition implementing <see cref="IEquatable{T}"/> of <see cref="Exception"/> to specify
    /// an exception.
    /// </summary>
    public class ExceptionSpecification : IEquatable<Exception>
    {
        private readonly Func<Exception, bool> _condition;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionSpecification"/> class with a sepcification of an
        /// exception.
        /// </summary>
        /// <param name="condition">To specify an exception.</param>
        public ExceptionSpecification(Func<Exception, bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            _condition = condition;
        }

        /// <summary>
        /// Gets a value indicating the condition passed from the constructor.
        /// </summary>
        public Func<Exception, bool> Condition
        {
            get
            {
                return _condition;
            }
        }

        /// <inheritdoc/>
        public bool Equals(Exception other)
        {
            return Condition(other);
        }
    }
}