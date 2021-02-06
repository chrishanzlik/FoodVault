using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Outbox;
using FoodVault.Framework.Infrastructure.Inbox;
using FoodVault.Framework.Infrastructure.InternalCommands;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace FoodVault.Modules.Storage.Infrastructure
{
    /// <summary>
    /// 'Storage' bounded-context database interface.
    /// </summary>
    public class StorageContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageContext" /> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public StorageContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FoodStorage> FoodStorages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorageContext).Assembly);
        }
    }
}
