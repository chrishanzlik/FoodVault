using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    public class StorageCannotBeSharedToTheOwnerRule : IDomainRule
    {
        private readonly UserId _ownerId;
        private readonly UserId _shareUserId;

        public StorageCannotBeSharedToTheOwnerRule(UserId ownerId, UserId shareUserId)
        {
            _ownerId = ownerId;
            _shareUserId = shareUserId;
        }

        public string Message => "The storage cannot be shared to the storages owner.";

        public bool Pass() => _ownerId != _shareUserId;
    }
}
