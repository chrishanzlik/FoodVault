using Autofac;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration
{
    internal static class StorageCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container)
        {
            _container = container;
        }

        internal static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}
