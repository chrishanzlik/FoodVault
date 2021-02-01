using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Framework.Infrastructure.InternalCommands
{
    /// <summary>
    /// Internal command EF type configuration.
    /// </summary>
    public sealed class InternalCommandEntityTypeConfig : IEntityTypeConfiguration<InternalCommand>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("InternalCommands");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id).ValueGeneratedNever();
            
            builder.Property(x => x.ProcessedDate)
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);
        }
    }
}
