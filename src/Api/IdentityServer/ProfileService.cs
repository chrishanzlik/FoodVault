using FoodVault.Modules.UserAccess.Application.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVault.Api.IdentityServer
{
    /// <summary>
    /// Identity server profile service
    /// </summary>
    internal class ProfileService : IProfileService
    {
        /// <inheritdoc />
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims.Where(x => x.Type == CustomClaimTypes.Roles).ToList());
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == CustomClaimTypes.FirstName));
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == CustomClaimTypes.Email));

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(context.IsActive);
        }
    }
}
