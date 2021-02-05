using System;

namespace FoodVault.Framework.Application.Commands
{
    public abstract class Command : ICommand
    {
        public Command()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
