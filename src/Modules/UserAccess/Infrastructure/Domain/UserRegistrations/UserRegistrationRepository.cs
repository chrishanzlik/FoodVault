using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Domain.UserRegistrations
{
    /// <summary>
    /// Repository for <see cref="UserRegistration"/>s.
    /// </summary>
    internal class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly UserAccessContext _userAccessContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegistrationRepository" /> class.
        /// </summary>
        /// <param name="userAccessContext">Db context.</param>
        public UserRegistrationRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(UserRegistration userRegistration)
        {
            await _userAccessContext.UserRegistrations.AddAsync(userRegistration);
        }

        /// <inheritdoc />
        public async Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId)
        {
            return await _userAccessContext.UserRegistrations.SingleOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}
