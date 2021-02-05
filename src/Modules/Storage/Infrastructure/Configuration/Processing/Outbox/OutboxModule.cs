using Autofac;
using FoodVault.Framework.Application.Outbox;
using FoodVault.Modules.Storage.Infrastructure.Outbox;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Outbox
{
    internal class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .InstancePerLifetimeScope();
        }
    }
}
