using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands.Results
{
    public class OkCommandResult : ICommandResult
    {
        internal OkCommandResult()
        {
            Success = true;
        }

        public bool Success { get; }
    }
}
