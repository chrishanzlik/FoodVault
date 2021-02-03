﻿using FoodVault.Modules.Storage.Domain.FoodStorages.Events;
using FoodVault.Modules.Storage.Domain.FoodStorages.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Products;

namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    /// <summary>
    /// Food storage entity.
    /// </summary>
    public class FoodStorage : Entity, IAggregateRoot
    {
        private readonly List<StoredProduct> _storedProducts = new List<StoredProduct>();

        private bool _isDeleted;

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private FoodStorage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStorage" /> class.
        /// </summary>
        /// <param name="storageName">Storage name.</param>
        /// <param name="description">Storage description.</param>
        public FoodStorage(string storageName, string description)
        {
            Id = new FoodStorageId(Guid.NewGuid());
            Name = storageName;
            Description = description;

            _isDeleted = false;

            this.AddDomainEvent(new FoodStorageCreatedEvent(Id));
        }

        /// <summary>
        /// Gets the id of the food storage.
        /// </summary>
        public FoodStorageId Id { get; }

        /// <summary>
        /// Gets the storage name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the storage description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a list of all stored products within this storage.
        /// </summary>
        public IReadOnlyCollection<StoredProduct> StoredProducts => _storedProducts.AsReadOnly();


        /// <summary>
        /// Removes a product from the storage.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="quantity">Quantity of items to remove.</param>
        /// /// <param name="expirationDate">Products expiration date.</param>
        public void RemoveProduct(ProductId productId, int quantity, DateTime? expirationDate)
        {
            var storedProduct = StoredProducts.FirstOrDefault(x => x.ProductId == productId && x.ExpirationDate == expirationDate);
            if (storedProduct == null)
            {
                return;
            }

            this.CheckDomainRule(new ProductOperationHasValidQuantityRule(quantity));
            this.CheckDomainRule(new HasEnaughProductQuantityToRemove(storedProduct.Quantity, quantity));

            if (storedProduct.Quantity - quantity <= 0)
            {
                this._storedProducts.Remove(storedProduct);
            }
            else
            {
                storedProduct.DecreaseQuantity(quantity);
            }

            this.AddDomainEvent(new ProductRemovedEvent(this.Id, productId, quantity));
        }

        /// <summary>
        /// Adds a product to the storage.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="quantity">Quantity of items to add.</param>
        /// <param name="expirationDate">Products expiration date.</param>
        /// <param name="productExistsChecker">Domain service that checks if a product exists.</param>
        public void StoreProduct(ProductId productId, int quantity, DateTime? expirationDate, IProductExistsChecker productExistsChecker)
        {
            this.CheckDomainRule(new ProductOperationHasValidQuantityRule(quantity));
            this.CheckDomainRule(new ProductExistsRule(productId, productExistsChecker));

            var storedProduct = StoredProducts.SingleOrDefault(x => x.ProductId == productId && x.ExpirationDate == expirationDate);

            if (storedProduct != null)
            {
                storedProduct.IncreaseQuantity(quantity);
            }
            else
            {
                this._storedProducts.Add(new StoredProduct(productId, quantity, expirationDate));
            }

            this.AddDomainEvent(new ProductStoredEvent(this.Id, productId, quantity));
        }

        /// <summary>
        /// Renames the storage and sets the description.
        /// </summary>
        public void Rename(string name, string description)
        {
            this.CheckDomainRule(new StorageNameMustBeMinFourCharactersLongRule(name));

            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Deletes the storage.
        /// </summary>
        public void Delete()
        {
            this._isDeleted = true;

            this.AddDomainEvent(new FoodStorageDeletedEvent(this.Id));
        }
    }
}