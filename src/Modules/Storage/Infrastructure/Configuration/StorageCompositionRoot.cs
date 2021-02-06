using Autofac;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration
{
    /// <summary>
    /// Composite root of the storage module.
    /// </summary>
    internal static class StorageCompositionRoot
    {
        private static IContainer _container;

        /// <summary>
        /// Sets the IoC container.
        /// </summary>
        /// <param name="container">Autofac container.</param>
        internal static void SetContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Begins a new lifetime scope with the attached autofac container.
        /// </summary>
        /// <returns>Autofac lifetime scope.</returns>
        internal static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}
