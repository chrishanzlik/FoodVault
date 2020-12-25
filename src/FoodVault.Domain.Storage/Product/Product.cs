using System;

namespace FoodVault.Domain.Storage.Product
{
    /// <summary>
    /// Product entity.
    /// </summary>
    public class Product : Entity, IAggregateRoot
    {
        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private Product()
        {
        }

        public Product(string productName)
        {
            //TODO: Guard clauses

            Id = new ProductId(Guid.NewGuid());
        }

        /// <summary>
        /// Gets the identifier of this object.
        /// </summary>
        public ProductId Id { get; }
    }
}
