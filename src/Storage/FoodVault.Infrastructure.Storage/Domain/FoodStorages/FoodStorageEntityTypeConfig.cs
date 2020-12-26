using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodVault.Infrastructure.Storage.Domain.FoodStorages
{
    internal sealed class FoodStorageEntityTypeConfig : IEntityTypeConfiguration<FoodStorage>
    {
        internal const string StoredProducts = "_storedProducts";

        public void Configure(EntityTypeBuilder<FoodStorage> builder)
        {
            builder.ToTable("FoodStorages");

            builder.HasKey(x => x.Id);

            builder.OwnsMany<StoredProduct>(StoredProducts, x =>
            {
                x.WithOwner().HasForeignKey("FoodStorageId");

                x.ToTable("StoredProducts");

                x.Property<ProductId>("ProductId");
                x.Property<FoodStorageId>("FoodStorageId");
                x.HasKey("FoodStorageId", "ProductId");
            });
        }
    }
}
