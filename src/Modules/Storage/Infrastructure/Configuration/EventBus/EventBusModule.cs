using Autofac;
using FoodVault.Framework.EventBus;
using FoodVault.Framework.Infrastructure.EventBus;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.EventBus
{
    internal class EventBusModule : Module
    {
        private readonly IEventBus _eventBus;

        public EventBusModule(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_eventBus != null)
            {
                builder.RegisterInstance(_eventBus).SingleInstance();
            }
            else
            {
                builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventBus>()
                .SingleInstance();
            }
        }
    }
}
