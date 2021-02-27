using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands.Results
{
    public class AuthenticatedCommandResult<TUser> : ICommandResult
    {
        internal AuthenticatedCommandResult(TUser user)
        {
            Success = true;
            Errors = new List<string>();
            User = user;
        }

        internal AuthenticatedCommandResult(string error)
        {
            Success = false;
            Errors = new List<string>() { error };
        }

        public bool Success { get; }

        public IEnumerable<string> Errors { get; }

        public TUser User { get; }
    }
}
