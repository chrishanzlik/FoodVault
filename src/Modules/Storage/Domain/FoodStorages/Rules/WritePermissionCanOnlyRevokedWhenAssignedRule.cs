using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    public class WritePermissionCanOnlyRevokedWhenAssignedRule : IDomainRule
    {
        private readonly bool _hasWritePermission;

        public WritePermissionCanOnlyRevokedWhenAssignedRule(bool hasWritePermission)
        {
            _hasWritePermission = hasWritePermission;
        }

        public string Message => "Cannot revoke write permission because this permission is not given.";

        public bool Pass() => _hasWritePermission == true;
    }
}
