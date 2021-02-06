using FoodVault.Modules.Storage.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodVault.Modules.Storage.Infrastructure.Domain.Products
{
    /// <summary>
    /// Product entity type configuration.
    /// </summary>
    internal sealed class ProductEntityTypeConfig : IEntityTypeConfiguration<Product>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "storage");

            builder.HasKey(x => x.Id);
        }
    }
}