using FoodVault.Modules.UserAccess.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace FoodVault.Api.Modules.UserAccess
{
    /// <summary>
    /// Builds URLs for the user access module.
    /// </summary>
    public class UserAccessModuleUrlBuilder : IUserAccessModuleUrlBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessModuleUrlBuilder" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor.</param>
        /// <param name="linkGenerator">Link generator.</param>
        public UserAccessModuleUrlBuilder(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        /// <inheritdoc />
        public string BuildConfirmationLink(Guid registrationId)
        {
            //TODO:
            return "TODO...";
        }
    }
}
