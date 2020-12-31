using System;
using System.Collections.Generic;

namespace FoodVault.Application.Mediator
{
    /// <summary>
    /// Represents the state of a executed command.
    /// </summary>
    public class CommandResult : ICommandResult
    {
        private CommandResult(bool success, Guid? entityId, IEnumerable<string> errors, CommandResultState state)
        {
            Success = success;
            Errors = errors;
            EntityId = entityId;
            State = state;
        }

        /// <inheritdoc />
        public bool Success { get; }

        /// <inheritdoc />
        public IEnumerable<string> Errors { get; }

        /// <inheritdoc />
        public Guid? EntityId { get; }

        /// <inheritdoc />
        public CommandResultState State { get; }


        /// <summary>
        /// Create a new error result.
        /// </summary>
        /// <param name="errors">List of errors.</param>
        /// <returns>Commands execution result.</returns>
        public static CommandResult Error(IEnumerable<string> errors)
        {
            return new CommandResult(false, null, errors, CommandResultState.Error);
        }

        /// <summary>
        /// Create a new entity created result.
        /// </summary>
        /// <param name="entityId">Entities id.</param>
        /// <returns>Commands execution result.</returns>
        public static CommandResult EntityCreated(Guid entityId)
        {
            return new CommandResult(true, entityId, null, CommandResultState.Created);
        }

        /// <summary>
        /// Create a new success result.
        /// </summary>
        /// <returns>Commands execution result.</returns>
        public static CommandResult Ok()
        {
            return new CommandResult(true, null, null, CommandResultState.Processed);
        }

        /// <summary>
        /// Create a new bad parameters result.
        /// </summary>
        /// <param name="errors">List of errors.</param>
        /// <returns>Commands execution result.</returns>
        public static CommandResult BadParameters(IEnumerable<string> errors)
        {
            return new CommandResult(false, null, errors, CommandResultState.BadParameters);
        }
    }
}
