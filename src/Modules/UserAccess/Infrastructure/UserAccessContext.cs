using Microsoft.EntityFrameworkCore;

namespace FoodVault.Modules.UserAccess.Infrastructure
{
    /// <summary>
    /// 'UserAccess' bounded-context database interface.
    /// </summary>
    public class UserAccessContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessContext" /> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public UserAccessContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessContext).Assembly);
        }
    }
}
