using FoodVault.Framework.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Modules.Storage.Infrastructure.Outbox
{
    /// <summary>
    /// Outbox message EF type configuration.
    /// </summary>
    public sealed class OutboxMessageEntityTypeConfig : IEntityTypeConfiguration<OutboxMessage>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", "storage");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.RaisingTime)
                .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

            builder.Property(x => x.ProcessedDate)
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);
        }
    }
}
