using FoodVault.Domain.Storage.Products;
using FoodVault.Infrastructure.Storage.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly StorageContext _storageContext;

        public ProductRepository(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(ProductId id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(ProductId id)
        {
            throw new NotImplementedException();
        }
    }
}
