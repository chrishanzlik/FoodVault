using FoodVault.Modules.UserAccess.Domain;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FoodVault.Modules.UserAccess.Infrastructure.Domain.UserRegistrations
{
    internal class UserRegistrationEntityTypeConfig : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("UserRegistrations", "users");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<DateTime?>("_confirmedAt")
                .HasColumnName("ConfirmedAt")
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : (DateTime?)null);

            builder.OwnsOne<EmailAddress>("_email", b =>
            {
                b.Property<string>("Value").HasColumnName("EmailAddress");
            });

            builder.OwnsOne<RegistrationState>("_state", b =>
            {
                b.Property<string>("Value").HasColumnName("State");
            });

            builder.OwnsOne<PasswordHash>("_passwordHash", b =>
            {
                b.Property<string>("Value").HasColumnName("PasswordHash");
            });
        }
    }
}
