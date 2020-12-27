using FoodVault.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FoodVault.Api.Storage.Common
{
    /// <summary>
    /// Extensions for <see cref="ICommandResult"/>.
    /// </summary>
    public static class ICommandResultExtensions
    {
        /// <summary>
        /// Converts a <see cref="ICommandResult"/> into a <see cref="IActionResult"/>.
        /// </summary>
        /// <param name="self">Result to convert.</param>
        /// <returns>IActionResult</returns>
        public static IActionResult ToActionResult(this ICommandResult self)
        {
            switch (self.State)
            {
                case CommandResultState.Processed:
                    return new OkResult();
                case CommandResultState.Created:
                    return new OkObjectResult(new { Id = self.EntityId });
                case CommandResultState.Error:
                    return new ObjectResult(self.Errors) { StatusCode = 500 };
                case CommandResultState.BadParameters:
                    return new BadRequestObjectResult(self.Errors);
                default:
                    throw new InvalidOperationException($"Cannot convert CommandResult state '{self.State}' into ActionResult.");
            }
        }
    }
}
