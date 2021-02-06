using Autofac.Core.Activators.Reflection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration
{
    /// <summary>
    /// Finds all constructors of an object.
    /// </summary>
    internal class ConstructorFinder : IConstructorFinder
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
            new ConcurrentDictionary<Type, ConstructorInfo[]>();

        /// <inheritdoc />
        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            var result = Cache.GetOrAdd(
                targetType,
                t => t.GetTypeInfo().DeclaredConstructors.ToArray());

            return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType);
        }
    }
}
