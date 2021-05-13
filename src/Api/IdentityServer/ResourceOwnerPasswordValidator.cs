using FoodVault.Framework.Application.Commands.Results;
using FoodVault.Modules.UserAccess.Application.Authentication.Authenticate;
using FoodVault.Modules.UserAccess.Application.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVault.Api.IdentityServer
{
    /// <summary>
    /// Validates passwords within the 'ResourceOwnerPassword' context.
    /// </summary>
    internal class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserAccessModule _userAccessModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOwnerPasswordValidator" /> class.
        /// </summary>
        /// <param name="userAccessModule">User access module.</param>
        public ResourceOwnerPasswordValidator(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        /// <inheritdoc />
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var commandResult = await _userAccessModule.ExecuteCommandAsync(
                new AuthenticateCommand(context.UserName, context.Password));

            var authenticationResult = commandResult as AuthenticatedCommandResult<UserDto>;

            if (authenticationResult == null || !commandResult.Success)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    authenticationResult.Error);

                return;
            }

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);
        }
    }
}
