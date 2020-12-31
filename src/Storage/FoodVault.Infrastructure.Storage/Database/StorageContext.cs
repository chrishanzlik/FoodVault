using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using FoodVault.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace FoodVault.Infrastructure.Storage.Database
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
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorageContext).Assembly);
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfig());
        }
    }
}
