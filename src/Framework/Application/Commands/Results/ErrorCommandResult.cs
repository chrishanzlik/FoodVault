using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands.Results
{
    public class ErrorCommandResult : ICommandResult
    {
        internal ErrorCommandResult(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
        }

        public bool Success { get; }

        public IEnumerable<string> Errors { get; }
    }
}
