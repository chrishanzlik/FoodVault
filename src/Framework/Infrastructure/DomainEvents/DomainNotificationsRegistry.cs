using System;
using System.Collections.Generic;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    public class DomainNotificationsRegistry : IDomainNotificationsRegistry
    {
        private readonly IDictionary<string, Type> _nameTypeMaps;
        private readonly IDictionary<Type, string> _typeNameMaps;

        public DomainNotificationsRegistry(IDictionary<string, Type> notificationMaps)
        {
            _typeNameMaps = new Dictionary<Type, string>();
            _nameTypeMaps = notificationMaps;

            foreach(var map in notificationMaps)
            {
                _typeNameMaps[map.Value] = map.Key;
            }
        }

        public string GetName(Type type)
        {
            return _typeNameMaps.TryGetValue(type, out string name) ? name : null;
        }

        public Type GetType(string name)
        {
            return _nameTypeMaps.TryGetValue(name, out Type type) ? type : null;
        }
    }
}
