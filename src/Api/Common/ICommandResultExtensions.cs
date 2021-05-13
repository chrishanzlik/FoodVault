using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Commands.Results;
using FoodVault.Modules.UserAccess.Application.Authentication.Authenticate;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FoodVault.Api.Common
{
    /// <summary>
    /// Extensions for <see cref="ICommandResult"/>.
    /// </summary>
    internal static class ICommandResultExtensions
    {
        /// <summary>
        /// Converts a <see cref="ICommandResult"/> into a <see cref="IActionResult"/>.
        /// </summary>
        /// <param name="self">Result to convert.</param>
        /// <returns>IActionResult</returns>
        public static IActionResult ToActionResult(this ICommandResult self)
        {
            switch (self)
            {
                case OkCommandResult:
                    return new OkResult();
                case EntityCreatedCommandResult entityCreatedCommandResult:
                    return new OkObjectResult(new { Id = entityCreatedCommandResult.EntityId });
                default:
                    throw new InvalidOperationException($"Not supported CommandResult.");
            }
        }
    }
}
