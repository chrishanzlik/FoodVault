using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using Microsoft.EntityFrameworkCore;

namespace FoodVault.Infrastructure.Storage.Database
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FoodStorage> FoodStorages { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorageContext).Assembly);
        }
    }
}
