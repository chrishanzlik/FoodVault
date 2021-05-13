using FoodVault.Modules.Storage.Domain.FoodStorages.Events;
using FoodVault.Modules.Storage.Domain.FoodStorages.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    /// <summary>
    /// Food storage entity.
    /// </summary>
    public class FoodStorage : Entity, IAggregateRoot
    {
        private readonly List<StoredProduct> _storedProducts = new List<StoredProduct>();
        private readonly List<StorageShare> _storageShares = new List<StorageShare>();

        private bool _isDeleted;

        private readonly UserId _ownerId;

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
        /// <param name="nameUniquessChecker">Checks that the choosen name is unique.</param>
        internal FoodStorage(UserId userId, string storageName, string description, IStorageNameUniquessChecker nameUniquessChecker)
        {
            this.CheckDomainRule(new StorageNameMustBeUniqueRule(storageName, userId, nameUniquessChecker));

            Id = new FoodStorageId(Guid.NewGuid());
            Name = storageName;
            Description = description;

            _ownerId = userId;
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
        /// Gets a list of all active storage shares.
        /// </summary>
        public IReadOnlyCollection<StorageShare> StorageShares => _storageShares.AsReadOnly();

        /// <summary>
        /// Creates a new <see cref="FoodStorage"/>.
        /// </summary>
        /// <param name="userId">Executing user id.</param>
        /// <param name="storageName">Name of the food storage.</param>
        /// <param name="description">Optional description of the food storage.</param>
        /// <param name="nameUniquessChecker">Domain service that checks that the choosen name is unique.</param>
        /// <returns>Created FoodStorage.</returns>
        public static FoodStorage CreateForUser(UserId userId, string storageName, string description, IStorageNameUniquessChecker nameUniquessChecker)
        {
            return new FoodStorage(userId, storageName, description, nameUniquessChecker);
        }

        /// <summary>
        /// Removes a product from the storage.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="quantity">Quantity of items to remove.</param>
        /// <param name="userContext">Informations about the executing user.</param>
        /// /// <param name="expirationDate">Products expiration date.</param>
        public void RemoveProduct(ProductId productId, int quantity, IUserContext userContext, DateTime? expirationDate)
        {
            this.CheckDomainRule(new HasWritePermissionRule(_ownerId, _storageShares, userContext));

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

            this.AddDomainEvent(new ProductRemovedEvent(this.Id, productId, userContext.UserId, quantity));
        }

        /// <summary>
        /// Adds a product to the storage.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="quantity">Quantity of items to add.</param>
        /// <param name="userContext">Informations about the executing user.</param>
        /// <param name="expirationDate">Products expiration date.</param>
        public void StoreProduct(ProductId productId, int quantity, IUserContext userContext, DateTime? expirationDate)
        {
            this.CheckDomainRule(new HasWritePermissionRule(_ownerId, _storageShares, userContext));
            this.CheckDomainRule(new ProductOperationHasValidQuantityRule(quantity));

            var storedProduct = StoredProducts.SingleOrDefault(x => x.ProductId == productId && x.ExpirationDate == expirationDate);

            if (storedProduct != null)
            {
                storedProduct.IncreaseQuantity(quantity);
            }
            else
            {
                this._storedProducts.Add(new StoredProduct(productId, quantity, expirationDate));
            }

            this.AddDomainEvent(new ProductStoredEvent(this.Id, productId, userContext.UserId, quantity));
        }

        /// <summary>
        /// Renames the storage and sets the description.
        /// </summary>
        /// <param name="storageName">New name of the storage.</param>
        /// <param name="storageDescription">New description of the storage.</param>
        /// <param name="userContext">Informations about the executing user.</param>
        /// <param name="nameUniquessChecker">Domain service that checks that the choosen name is unique.</param>
        public void Rename(string storageName, string storageDescription, IUserContext userContext, IStorageNameUniquessChecker nameUniquessChecker)
        {
            this.CheckDomainRule(new RequiresToBeStorageOwnerRule(_ownerId, userContext));

            if (!storageName.Equals(this.Name, StringComparison.OrdinalIgnoreCase))
            {
                this.CheckDomainRule(new StorageNameMustBeUniqueRule(storageName, _ownerId, nameUniquessChecker));
            }
            
            this.Name = storageName;
            this.Description = storageDescription;
        }

        /// <summary>
        /// Deletes the storage.
        /// </summary>
        /// <param name="userContext">Informations about the executing user.</param>
        public void Delete(IUserContext userContext)
        {
            this.CheckDomainRule(new RequiresToBeStorageOwnerRule(_ownerId, userContext));

            this._isDeleted = true;

            this.AddDomainEvent(new FoodStorageDeletedEvent(this.Id, userContext.UserId));
        }

        /// <summary>
        /// Shares the storage with another user.
        /// </summary>
        /// <param name="userId">Users identifer to share the storage with.</param>
        /// <param name="hasWritePermission">If the user can add or remove storage items.</param>
        /// <param name="userSharesFinder">Domain services that checks if the storage is already shared to the given user id.</param>
        /// <param name="userContext">Informations about the executing user.</param>
        public void ShareToUser(UserId userId, bool hasWritePermission, IEnumerable<UserId> sharedStorageUsers, IUserContext userContext)
        {
            this.CheckDomainRule(new StorageCannotBeSharedToTheOwnerRule(_ownerId, userId));
            this.CheckDomainRule(new RequiresToBeStorageOwnerRule(_ownerId, userContext));
            this.CheckDomainRule(new StorageIsNotAlreadySharedToUserRule(userId, sharedStorageUsers));

            _storageShares.Add(StorageShare.CreateForUser(Id, userId, hasWritePermission));

            this.AddDomainEvent(new StorageShareCreatedEvent(userId, Id, hasWritePermission));
        }

        /// <summary>
        /// Remove a storage share for a user.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="userContext">Informations about the executing user.</param>
        public void Unshare(UserId userId, IUserContext userContext)
        {
            this.CheckDomainRule(new RequiresToBeStorageOwnerRule(_ownerId, userContext));

            var toRemove = this.StorageShares.FirstOrDefault(x => x.UserId == userId);
            if (toRemove != null)
            {
                _storageShares.Remove(toRemove);

                this.AddDomainEvent(new StorageShareRemovedEvent(userId, Id));
            }
        }
    }
}
