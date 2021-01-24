using System;

namespace FoodVault.Application.Commands
{
    /// <summary>
    /// Internal application command for integration handling.
    /// </summary>
    public abstract class InternalCommandBase : ICommand
    {
        protected InternalCommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifer of the internal command.
        /// </summary>
        public Guid Id { get; }
    }
}
