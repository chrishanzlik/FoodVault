using Autofac;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration
{
    /// <summary>
    /// Composite root of the user access module.
    /// </summary>
    internal static class UserAccessCompositionRoot
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
