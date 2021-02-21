using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    /// <summary>
    /// Repository interface for <see cref="UserRegistration"/>s.
    /// </summary>
    public interface IUserRegistrationRepository
    {
        /// <summary>
        /// Adds a <see cref="UserRegistration"/> to the repository.
        /// </summary>
        /// <param name="userRegistration">Registration to add.</param>
        /// <returns>Awaitable task.</returns>
        Task AddAsync(UserRegistration userRegistration);

        /// <summary>
        /// Gets a single <see cref="UserRegistration"/> by its identifier from the repository.
        /// </summary>
        /// <param name="userRegistrationId">Identifier of the registration to fetch.</param>
        /// <returns>Found registration or null.</returns>
        Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId);
    }
}
