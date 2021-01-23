using FoodVault.Application.FileUploads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
