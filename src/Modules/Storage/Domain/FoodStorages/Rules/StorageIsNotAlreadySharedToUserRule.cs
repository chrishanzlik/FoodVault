using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    public class StorageIsNotAlreadySharedToUserRule : IDomainRule
    {
        private readonly UserId _userId;
        private readonly IEnumerable<UserId> _sharedStorageUsers;

        public StorageIsNotAlreadySharedToUserRule(UserId userId, IEnumerable<UserId> sharedStorageUsers)
        {
            _userId = userId;
            _sharedStorageUsers = sharedStorageUsers;
        }

        public string Message => $"The storage is already shared to the user '{_userId}'";

        public bool Pass() => !_sharedStorageUsers.Any(x => x == _userId);
    }
}
