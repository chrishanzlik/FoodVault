using FoodVault.Application.FileUploads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Infrastructure.FileUploads
{
    /// <summary>
    /// File upload EF type configuration.
    /// </summary>
    public sealed class FileUploadEntityTypeConfig : IEntityTypeConfiguration<FileUpload>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<FileUpload> builder)
        {
            builder.ToTable("FileUploads");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.UploadTime)
                .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
            
            builder.Property(x => x.ExpirationTime)
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);

            builder.Property(x => x.ContentType);

            builder.Property(x => x.Extension);

            builder.Property(x => x.RelativeFileLocation);

            builder.Property(x => x.Size);
        }
    }
}
