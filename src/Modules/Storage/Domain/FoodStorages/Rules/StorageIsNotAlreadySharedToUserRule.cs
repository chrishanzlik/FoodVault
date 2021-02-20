using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;
using System.Linq;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    public class StorageIsNotAlreadySharedToUserRule : IDomainRule
    {
        private readonly FoodStorageId _foodStorageId;
        private readonly UserId _userId;
        private readonly IStorageUserSharesFinder _storageUserSharesFinder;

        public StorageIsNotAlreadySharedToUserRule(FoodStorageId foodStorageId, UserId userId, IStorageUserSharesFinder storageUserSharesFinder)
        {
            _foodStorageId = foodStorageId;
            _userId = userId;
            _storageUserSharesFinder = storageUserSharesFinder;
        }

        public string Message => $"The storage '{_foodStorageId}' is already shared to the user '{_userId}'";

        public bool Pass() => !_storageUserSharesFinder.GetSharedUserIdsForStorage(_foodStorageId).Any(x => x == _userId);
    }
}
