using FoodVault.Framework.Infrastructure.Inbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Modules.UserAccess.Infrastructure.Inbox
{
    /// <summary>
    /// Outbox message EF type configuration.
    /// </summary>
    internal sealed class OutboxMessageEntityTypeConfig : IEntityTypeConfiguration<InboxMessage>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("InboxMessages", "users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.RaisingTime)
                .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

            builder.Property(x => x.ProcessedDate)
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);
        }
    }
}
