using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Domain.Users
{
    /// <summary>
    /// Repository interface for the <see cref="User"/> aggregate.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a <see cref="User"/> to the repository.
        /// </summary>
        /// <param name="user">The user which should be added to the repository.</param>
        /// <returns>Awaitable task.</returns>
        Task AddAsync(User user);
    }
}
