using System;

namespace FoodVault.Domain.Storage.FoodStore
{
    /// <summary>
    /// Identifier for a <see cref="FoodStore"/> object.
    /// </summary>
    public sealed class FoodStoreId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStoreId" /> class.
        /// </summary>
        /// <param name="value">Identifiers value.</param>
        public FoodStoreId(Guid value) : base(value)
        {
        }
    }
}
