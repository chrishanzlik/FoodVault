using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Infrastructure.Storage.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Domain.FoodStorages
{
    public class FoodStorageRepository : IFoodStoreRepository
    {
        private readonly StorageContext _storageContext;

        public FoodStorageRepository(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task AddAsync(FoodStorage store)
        {
            throw new NotImplementedException();
        }

        public Task<FoodStorage> GetByIdAsync(FoodStorageId id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(FoodStorageId id)
        {
            throw new NotImplementedException();
        }
    }
}
