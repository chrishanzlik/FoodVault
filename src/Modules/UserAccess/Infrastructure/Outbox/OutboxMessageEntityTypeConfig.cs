using FoodVault.Framework.Application.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Modules.UserAccess.Infrastructure.Outbox
{
    /// <summary>
    /// Outbox message EF type configuration.
    /// </summary>
    internal sealed class OutboxMessageEntityTypeConfig : IEntityTypeConfiguration<OutboxMessage>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", "users");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.RaisingTime)
                .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

            builder.Property(x => x.ProcessedDate)
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);
        }
    }
}
