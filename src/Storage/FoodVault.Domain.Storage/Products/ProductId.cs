using System;

namespace FoodVault.Domain.Storage.Products
{
    /// <summary>
    /// Identifier for a <see cref="Product"/> object.
    /// </summary>
    public sealed class ProductId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductId" /> class.
        /// </summary>
        /// <param name="value">Identifiers value.</param>
        public ProductId(Guid value) : base(value)
        {
        }
    }
}
