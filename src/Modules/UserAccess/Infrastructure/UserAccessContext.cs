using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Outbox;
using FoodVault.Framework.Infrastructure.Inbox;
using FoodVault.Framework.Infrastructure.InternalCommands;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using FoodVault.Modules.UserAccess.Domain.Users;
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

        public DbSet<User> Users { get; set; }
        public DbSet<UserRegistration> UserRegistrations { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessContext).Assembly);
        }
    }
}
