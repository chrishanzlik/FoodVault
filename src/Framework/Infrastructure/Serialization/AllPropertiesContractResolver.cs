using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FoodVault.Framework.Infrastructure.Serialization
{
    /// <summary>
    /// Contract resolver for Newtonsoft.Json, that resolves all properites from a given type.
    /// </summary>
    public class AllPropertiesContractResolver : DefaultContractResolver
    {
        /// <inheritdoc />
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = type.GetProperties(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance)
                .Select(p => this.CreateProperty(p, memberSerialization))
                .ToList();

            properties.ForEach(p =>
            {
                p.Writable = true;
                p.Readable = true;
            });

            return properties;
        }
    }
}
