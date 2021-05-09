using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.Configuration.Authorization
{
    /// <summary>
    /// Abstract attribute authorization handler.
    /// </summary>
    /// <typeparam name="TRequirement">Type of the authorization requirement.</typeparam>
    /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
        where TAttribute : Attribute
    {
        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attribute = (context.Resource as RouteEndpoint)?.Metadata.GetMetadata<TAttribute>();

            return HandleRequirementAsync(context, requirement, attribute);
        }

        /// <inheritdoc />
        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TRequirement requirement,
            TAttribute attribute);
    }
}
