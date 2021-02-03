using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Domain.FoodStorages
{
    /// <summary>
    /// Repository for interacting with <see cref="FoodStorage"/>s.
    /// </summary>
    internal class FoodStorageRepository : IFoodStorageRepository
    {
        private readonly StorageContext _storageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStorageRepository" /> class.
        /// </summary>
        /// <param name="storageContext">Database connection.</param>
        public FoodStorageRepository(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(FoodStorage store)
        {
            await _storageContext.FoodStorages.AddAsync(store);
        }

        /// <inheritdoc />
        public async Task<FoodStorage> GetByIdAsync(FoodStorageId id)
        {
            return await _storageContext.FoodStorages
                .Include(x => x.StoredProducts)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(FoodStorageId id)
        {
            var storage = await GetByIdAsync(id);

            if (storage != null)
            {
                _storageContext.FoodStorages.Remove(storage);
            }
        }
    }
}
