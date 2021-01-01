using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodVault.Infrastructure.Outbox
{
    /// <summary>
    /// Outbox message EF type configuration.
    /// </summary>
    public sealed class OutboxMessageEntityTypeConfig : IEntityTypeConfiguration<OutboxMessage>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
