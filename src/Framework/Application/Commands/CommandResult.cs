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
        /// Create a new authenticated result.
        /// </summary>
        /// <typeparam name="TUser">User type.</typeparam>
        /// <param name="user">User.</param>
        /// <returns>Command execution result</returns>
        public static ICommandResult Authenticated<TUser>(TUser user)
        {
            return new AuthenticatedCommandResult<TUser>(user);
        }

        /// <summary>
        /// Create a new authenticated result.
        /// </summary>
        /// <typeparam name="TUser">User type.</typeparam>
        /// <param name="error">Error.</param>
        /// <returns>Command execution result</returns>
        public static ICommandResult AuthenticationFailed<TUser>(string error)
        {
            return new AuthenticatedCommandResult<TUser>(error);
        }
    }
}
