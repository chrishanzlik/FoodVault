using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodVault.Infrastructure.InternalCommands
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
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
        }
    }
}
