using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands.Results
{
    public class InvalidParametersCommandResult : ICommandResult
    {
        internal InvalidParametersCommandResult(IEnumerable<string> errors)
        {
            Errors = errors;
            Success = false;
        }

        public bool Success { get; }

        public IEnumerable<string> Errors { get; }
    }
}
