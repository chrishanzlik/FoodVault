using FoodVault.Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FoodVault.Api.Configuration.Validation
{
    /// <summary>
    /// Describes a specific <see cref="InvalidCommandException"/>.
    /// </summary>
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandProblemDetails" /> class.
        /// </summary>
        /// <param name="exception">Occured error.</param>
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = "Command validation error";
            Status = StatusCodes.Status400BadRequest;
            Type = "https://foodvault/validation-error";
            Errors = exception.Errors;
        }

        /// <summary>
        /// Gets all validation errors.
        /// </summary>
        public List<string> Errors { get; }
    }
}
