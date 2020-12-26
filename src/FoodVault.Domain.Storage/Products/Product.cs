using System;

namespace FoodVault.Domain.Storage.Products
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
            Id = new ProductId(Guid.NewGuid());
            Name = productName;
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public ProductId Id { get; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; private set; }
    }
}
