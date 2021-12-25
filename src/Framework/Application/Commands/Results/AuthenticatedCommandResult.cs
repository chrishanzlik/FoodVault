namespace FoodVault.Framework.Application.Commands.Results
{
    public class AuthenticatedCommandResult<TUser> : ICommandResult
    {
        internal AuthenticatedCommandResult(TUser user)
        {
            Success = true;
            User = user;
        }

        internal AuthenticatedCommandResult(string error)
        {
            Success = false;
            Error = error;
        }

        public bool Success { get; }

        public TUser User { get; }

        public string Error { get; }
    }
}
