using FoodVault.Modules.UserAccess.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FoodVault.Api.Modules.UserAccess
{
    public class UserAccessModuleUrlBuilder : IUserAccessModuleUrlBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public UserAccessModuleUrlBuilder(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }
    }
}
