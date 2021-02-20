using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    public class WritePermissionCanOnlyGrantedWhenNotAssignedRule : IDomainRule
    {
        private readonly bool _hasWritePermission;

        public WritePermissionCanOnlyGrantedWhenNotAssignedRule(bool hasWritePermission)
        {
            _hasWritePermission = hasWritePermission;
        }

        public string Message => "Cannot set write permission because it is already granted.";

        public bool Pass() => _hasWritePermission == false;
    }
}
