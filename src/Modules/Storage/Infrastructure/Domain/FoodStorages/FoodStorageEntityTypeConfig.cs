using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Modules.Storage.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Modules.Storage.Infrastructure.Domain.FoodStorages
{
    /// <summary>
    /// Food storage entity type configuration.
    /// </summary>
    internal sealed class FoodStorageEntityTypeConfig : IEntityTypeConfiguration<FoodStorage>
    {
        internal const string StoredProducts = "StoredProducts";
        internal const string StorageShares = "StorageShares";

        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<FoodStorage> builder)
        {
            builder.ToTable("FoodStorages", "storage");

            builder.HasKey(x => x.Id);

            builder.Property<bool>("_isDeleted").HasColumnName("IsDeleted");

            builder.Property<UserId>("_ownerId").HasColumnName("OwnerId");

            builder.OwnsMany<StoredProduct>(StoredProducts, x =>
            {
                x.WithOwner().HasForeignKey("FoodStorageId");

                x.ToTable("StoredProducts", "storage");

                x.Property<ProductId>("ProductId");

                x.Property<FoodStorageId>("FoodStorageId");

                x.Property<Guid>("Id").ValueGeneratedOnAdd();
                
                x.HasKey("Id");

                x.Property<DateTime?>("ExpirationDate")
                    .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);
            });

            builder.OwnsMany<StorageShare>(StorageShares, x =>
            {
                x.WithOwner().HasForeignKey("FoodStorageId");

                x.ToTable("StorageShares", "storage");

                x.Property<FoodStorageId>("FoodStorageId");

                x.Property<UserId>("_userId").HasColumnName("UserId");

                x.Property<bool>("_canWrite").HasColumnName("CanWrite");

                x.Property<Guid>("Id").ValueGeneratedOnAdd();

                x.HasKey("Id");

                x.Property<DateTime>("SharedAt")
                    .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
            });
        }
    }
}
