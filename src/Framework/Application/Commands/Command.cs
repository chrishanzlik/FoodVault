using System;

namespace FoodVault.Framework.Application.Commands
{
    /// <summary>
    /// Base class where commands derive from.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        public Command()
        {
            Id = Guid.NewGuid();
        }

        /// <inheritdoc />
        public Guid Id { get; }
    }
}
