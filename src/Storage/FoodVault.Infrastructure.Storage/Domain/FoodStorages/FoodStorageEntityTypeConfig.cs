using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Infrastructure.Storage.Domain.FoodStorages
{
    internal sealed class FoodStorageEntityTypeConfig : IEntityTypeConfiguration<FoodStorage>
    {
        internal const string StoredProducts = "StoredProducts";

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

                x.Property<Guid>("Id").ValueGeneratedOnAdd();
                
                x.HasKey("Id");

                x.Property<DateTime?>("ExpirationDate")
                    .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);
            });
        }
    }
}
