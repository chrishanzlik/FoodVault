using FoodVault.Framework.Domain;
using System;

namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    /// <summary>
    /// Identifier for a <see cref="FoodStorage"/> object.
    /// </summary>
    public sealed class FoodStorageId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStorageId" /> class.
        /// </summary>
        /// <param name="value">Identifiers value.</param>
        public FoodStorageId(Guid value) : base(value)
        {
        }
    }
}
