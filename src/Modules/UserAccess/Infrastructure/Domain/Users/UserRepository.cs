using FoodVault.Modules.UserAccess.Domain.Users;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Domain.Users
{
    /// <summary>
    /// Repository for <see cref="User"/>s.
    /// </summary>
    internal class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _userAccessContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="userAccessContext">Database connection.</param>
        public UserRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(User user)
        {
            await _userAccessContext.Users.AddAsync(user);
        }
    }
}
