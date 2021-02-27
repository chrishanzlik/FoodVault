using FoodVault.Framework.Application.Commands.Results;
using System;
using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands
{
    /// <summary>
    /// Represents the state of a executed command.
    /// </summary>
    public static class CommandResult
    {
        /// <summary>
        /// Create a new error result.
        /// </summary>
        /// <param name="error">Error.</param>
        /// <returns>Commands execution result.</returns>
        public static ICommandResult Error(string error)
        {
            return new ErrorCommandResult(new string[] { error });
        }

        /// <summary>
        /// Create a new error result.
        /// </summary>
        /// <param name="errors">List of errors.</param>
        /// <returns>Commands execution result.</returns>
        public static ICommandResult Error(IEnumerable<string> errors)
        {
            return new ErrorCommandResult(errors);
        }

        /// <summary>
        /// Create a new entity created result.
        /// </summary>
        /// <param name="entityId">Entities id.</param>
        /// <returns>Commands execution result.</returns>
        public static ICommandResult EntityCreated(Guid entityId)
        {
            return new EntityCreatedCommandResult(entityId);
        }

        /// <summary>
        /// Create a new success result.
        /// </summary>
        /// <returns>Commands execution result.</returns>
        public static ICommandResult Ok()
        {
            return new OkCommandResult();
        }

        /// <summary>
        /// Create a new bad parameters result.
        /// </summary>
        /// <param name="errors">List of errors.</param>
        /// <returns>Commands execution result.</returns>
        public static ICommandResult BadParameters(IEnumerable<string> errors)
        {
            return new InvalidParametersCommandResult(errors);
        }

        public static ICommandResult Authenticated<TUser>(TUser user)
        {
            return new AuthenticatedCommandResult<TUser>(user);
        }

        public static ICommandResult AuthenticationFailed<TUser>(string error)
        {
            return new AuthenticatedCommandResult<TUser>(error);
        }
    }
}
