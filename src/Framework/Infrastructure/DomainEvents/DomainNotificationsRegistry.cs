using System;
using System.Collections.Generic;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    /// <summary>
    /// Contains mappings between name and type of domain notifications.
    /// </summary>
    public class DomainNotificationsRegistry : IDomainNotificationsRegistry
    {
        private readonly IDictionary<string, Type> _nameTypeMaps;
        private readonly IDictionary<Type, string> _typeNameMaps;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="notificationMaps">Mappings inside a dictionary.</param>
        public DomainNotificationsRegistry(IDictionary<string, Type> notificationMaps)
        {
            _typeNameMaps = new Dictionary<Type, string>();
            _nameTypeMaps = notificationMaps;

            foreach(var map in notificationMaps)
            {
                _typeNameMaps[map.Value] = map.Key;
            }
        }

        /// <inheritdoc />
        public string GetName(Type type)
        {
            return _typeNameMaps.TryGetValue(type, out string name) ? name : null;
        }

        /// <inheritdoc />
        public Type GetType(string name)
        {
            return _nameTypeMaps.TryGetValue(name, out Type type) ? type : null;
        }
    }
}
