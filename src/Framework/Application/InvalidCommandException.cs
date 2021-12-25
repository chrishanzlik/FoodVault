using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Framework.Application
{
    /// <summary>
    /// Indicates an invalid command.
    /// </summary>
    public class InvalidCommandException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException" /> class.
        /// </summary>
        /// <param name="errors">A list of errors.</param>
        public InvalidCommandException(List<string> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException" /> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public InvalidCommandException(string error) : this(new List<string> { error })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException" /> class.
        /// </summary>
        /// <param name="errors">An enumerable of errors</param>
        public InvalidCommandException(IEnumerable<string> errors) : this (errors.ToList())
        {
        }

        /// <summary>
        /// Gets a list of errors.
        /// </summary>
        public List<string> Errors { get; }
    }
}
