using Autofac;
using FoodVault.Modules.UserAccess.Application.Authentication;
using FoodVault.Modules.UserAccess.Application.UserRegistrations.DomainServices;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Domain
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordManager>()
                .As<IPasswordManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailFreeChecker>()
                .As<IEmailFreeChecker>()
                .InstancePerLifetimeScope();
        }
    }
}
