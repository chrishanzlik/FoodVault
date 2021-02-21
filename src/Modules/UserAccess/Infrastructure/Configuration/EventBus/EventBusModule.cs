using Autofac;
using FoodVault.Framework.EventBus;
using FoodVault.Framework.Infrastructure.EventBus;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.EventBus
{
    /// <summary>
    /// IoC container module for 'EventBus'
    /// </summary>
    internal class EventBusModule : Module
    {
        private readonly IEventBus _eventBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBusModule" /> class.
        /// </summary>
        /// <param name="eventBus">Modules event bus.</param>
        public EventBusModule(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <inheritdoc />
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
