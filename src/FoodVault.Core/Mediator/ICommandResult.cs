using System;
using System.Collections.Generic;

namespace FoodVault.Core.Mediator
{
    /// <summary>
    /// Represents the state of a executed command.
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// Gets a value indicating whether the command was executed successful.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets a list of errors which occurs at execution time.
        /// </summary>
        IEnumerable<string> Errors { get; }

        /// <summary>
        /// Gets the entity id, if the commands purpose was to create a new entity.
        /// </summary>
        Guid? EntityId { get; }

        /// <summary>
        /// Gets the commands state.
        /// </summary>
        CommandResultState State { get; }
    }
}
