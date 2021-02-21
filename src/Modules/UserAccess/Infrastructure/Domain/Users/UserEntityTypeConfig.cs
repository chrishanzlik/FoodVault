using FoodVault.Modules.UserAccess.Domain;
using FoodVault.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodVault.Modules.UserAccess.Infrastructure.Domain.Users
{
    /// <summary>
    /// Product entity type configuration.
    /// </summary>
    internal sealed class UserEntityTypeConfig : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "users");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_firstName").HasColumnName("FirstName");

            builder.Property<string>("_lastName").HasColumnName("LastName");

            builder.Property<bool>("_isActive").HasColumnName("IsActive");

            builder.OwnsOne<PasswordHash>("_passwordHash", b =>
            {
                b.Property(x => x.Value).HasColumnName("PasswordHash");
            });

            builder.OwnsOne<EmailAddress>("_email", b =>
            {
                b.Property(x => x.Value).HasColumnName("EmailAddress");
            });

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.WithOwner().HasForeignKey("UserId");
                b.ToTable("UserRoles", "users");
                b.Property<UserId>("UserId");
                b.Property<string>("Value").HasColumnName("RoleName");
                b.HasKey("UserId", "Value");
            });
        }
    }
}
