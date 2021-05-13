using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands
{
    public interface ICommandResult
    {
        public bool Success { get; }
    }
}
