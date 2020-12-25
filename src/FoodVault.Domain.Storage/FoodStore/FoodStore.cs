using FoodVault.Domain.Storage.FoodStore.Events;
using FoodVault.Domain.Storage.FoodStore.Rules;
using FoodVault.Domain.Storage.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Domain.Storage.FoodStore
{
    /// <summary>
    /// Food storage entity.
    /// </summary>
    public class FoodStore : Entity, IAggregateRoot
    {
        private readonly List<StoredProduct> _storedProducts = new List<StoredProduct>();

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private FoodStore()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStore" /> class.
        /// </summary>
        /// <param name="storageName">Storage name.</param>
        /// <param name="description">Storage description.</param>
        public FoodStore(string storageName, string description)
        {
            Id = new FoodStoreId(Guid.NewGuid());
            Name = storageName;
            Description = description;

            this.AddDomainEvent(new FoodStoreCreatedEvent(Id));
        }

        /// <summary>
        /// Gets the stores identifier.
        /// </summary>
        public FoodStoreId Id { get; }

        /// <summary>
        /// Gets the storage name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the storage description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a list of all stored products within this store.
        /// </summary>
        public IReadOnlyCollection<StoredProduct> StoredProducts => _storedProducts.AsReadOnly();


        /// <summary>
        /// Removes a product from the store.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="quantity">Quantity of items to remove.</param>
        public void RemoveProduct(ProductId productId, int quantity)
        {
            this.CheckDomainRule(new ProductOperationHasValidQuantityRule(quantity));

            var storedProduct = StoredProducts.Single(x => x.ProductId == productId);

            this.CheckDomainRule(new ProductHasEnaughQuantityToRemove(storedProduct.Quantity, quantity));

            if (storedProduct.Quantity - quantity <= 0)
            {
                this._storedProducts.Remove(storedProduct);
            }
            else
            {
                storedProduct.Quantity -= quantity;
            }

            this.AddDomainEvent(new ProductRemovedEvent(this.Id, productId, quantity));
        }

        /// <summary>
        /// Adds a product to the store.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="quantity">Quantity of items to add.</param>
        public void StoreProduct(ProductId productId, int quantity)
        {
            this.CheckDomainRule(new ProductOperationHasValidQuantityRule(quantity));

            var existingEntry = StoredProducts.SingleOrDefault(x => x.ProductId == productId);

            if (existingEntry != null)
            {
                existingEntry.Quantity += quantity;
            }
            else
            {
                this._storedProducts.Add(new StoredProduct(this.Id, productId, quantity));
            }

            this.AddDomainEvent(new ProductStoredEvent(this.Id, productId, quantity));
        }

        /// <summary>
        /// Renames the store and sets the description.
        /// </summary>
        public void Rename(string name, string description)
        {
            this.CheckDomainRule(new StorageNameMustBeMinFourCharactersLongRule(name));

            this.Name = name;
            this.Description = description;
        }
    }
}
