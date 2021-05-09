using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands.Results
{
    public class OkCommandResult : ICommandResult
    {
        internal OkCommandResult()
        {
            Success = true;
            Errors = new List<string>();
        }

        public bool Success { get; }

        public IEnumerable<string> Errors { get; }
    }
}
