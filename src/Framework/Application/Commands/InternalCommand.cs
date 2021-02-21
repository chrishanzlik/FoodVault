using System;

namespace FoodVault.Framework.Application.Commands
{
    /// <summary>
    /// Internal application command for integration handling.
    /// </summary>
    public abstract class InternalCommand : ICommand
    {
        public InternalCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
