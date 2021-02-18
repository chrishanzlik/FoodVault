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

            builder.OwnsOne<EmailAddress>("_email", emailBuilder =>
            {
                emailBuilder.Property(x => x.EmailValue).HasColumnName("EmailAddress");
            });

            builder.OwnsMany<UserRole>("_roles", roleBuilder =>
            {
                roleBuilder.WithOwner().HasForeignKey("UserId");
                roleBuilder.ToTable("UserRoles", "users");
                roleBuilder.Property<UserId>("UserId");
                roleBuilder.Property<string>("Value").HasColumnName("RoleName");
                roleBuilder.HasKey("UserId", "Value");
            });
        }
    }
}
