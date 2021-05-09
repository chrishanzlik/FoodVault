using FoodVault.Api.Common;
using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.UserAccess.Application.Contracts;
using FoodVault.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration;
using FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.Modules.UserAccess.UserRegistrations
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegistrationsController
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserRegistrationsController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName);

            ICommandResult result = await _userAccessModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        [HttpPatch("{registrationId}/confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmUserRegistrationAsync([FromRoute] Guid registrationId)
        {
            var command = new ConfirmUserRegistrationCommand(registrationId);

            ICommandResult result = await _userAccessModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }
    }
}
