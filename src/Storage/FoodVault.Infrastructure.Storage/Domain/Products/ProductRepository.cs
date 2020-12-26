﻿using FoodVault.Domain.Storage.Products;
using FoodVault.Infrastructure.Storage.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Domain.Products
{
    /// <summary>
    /// Repository for interacting with <see cref="Product"/>s.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly StorageContext _storageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="storageContext">Database connection.</param>
        public ProductRepository(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(Product product)
        {
            await _storageContext.Products.AddAsync(product);
        }

        /// <inheritdoc />
        public async Task<Product> GetByIdAsync(ProductId id)
        {
            return await _storageContext.Products
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(ProductId id)
        {
            var product = await GetByIdAsync(id);

            if (product != null)
            {
                _storageContext.Products.Remove(product);
            }
        }
    }
}
