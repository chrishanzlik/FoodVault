using FoodVault.Framework.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace FoodVault.Framework.Infrastructure.Database
{
    /// <summary>
    /// Converter between <see cref="EntityId"/> types and <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntityId">Concrete type of the entity identifier.</typeparam>
    public class EntityIdValueConverter<TEntityId> : ValueConverter<TEntityId, Guid>
        where TEntityId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIdValueConverter" /> class.
        /// </summary>
        /// <param name="mappingHints">Mapping hints.</param>
        public EntityIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => Factory(value), mappingHints)
        {
        }

        private static TEntityId Factory(Guid id) => Activator.CreateInstance(typeof(TEntityId), new object[] { id }) as TEntityId;
    }
}
