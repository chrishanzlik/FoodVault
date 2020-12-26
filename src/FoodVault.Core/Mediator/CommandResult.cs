using System;
using System.Collections.Generic;

namespace FoodVault.Core.Mediator
{
    /// <summary>
    /// Represents the state of a executed command.
    /// </summary>
    public class CommandResult : ICommandResult
    {
        private CommandResult(bool success, Guid? entityId, IEnumerable<string> errors)
        {
            Success = success;
            Errors = errors;
            EntityId = entityId;
        }

        /// <inheritdoc />
        public bool Success { get; }

        /// <inheritdoc />
        public IEnumerable<string> Errors { get; }

        /// <inheritdoc />
        public Guid? EntityId { get; }


        /// <summary>
        /// Create a new error result.
        /// </summary>
        /// <param name="errors">List of errors.</param>
        /// <returns>Commands execution result.</returns>
        public static CommandResult Failed(IEnumerable<string> errors)
        {
            return new CommandResult(false, null, errors);
        }

        /// <summary>
        /// Create a new entity created result.
        /// </summary>
        /// <param name="entityId">Entities id.</param>
        /// <returns>Commands execution result.</returns>
        public static CommandResult EntityCreated(Guid entityId)
        {
            return new CommandResult(true, entityId, null);
        }

        /// <summary>
        /// Create a new success result.
        /// </summary>
        /// <returns>Commands execution result.</returns>
        public static CommandResult Ok()
        {
            return new CommandResult(true, null, null);
        }
    }
}
