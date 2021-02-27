using FoodVault.Framework.Application.Commands.Results;
using FoodVault.Modules.UserAccess.Application.Authentication.Authenticate;
using FoodVault.Modules.UserAccess.Application.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVault.Api.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserAccessModule _userAccessModule;

        public ResourceOwnerPasswordValidator(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var commandResult = await _userAccessModule.ExecuteCommandAsync(
                new AuthenticateCommand(context.UserName, context.Password));

            var authenticationResult = commandResult as AuthenticatedCommandResult<UserDto>;

            if (authenticationResult == null || !commandResult.Success)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    commandResult.Errors.Single());

                return;
            }

            if (authenticationResult != null || !commandResult.Success)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    commandResult.Errors.Single());

                return;
            }

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);
        }
    }
}
