using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Ensures that the executing user is the storage owner.
    /// </summary>
    public class RequiresToBeStorageOwnerRule : IDomainRule
    {
        private readonly UserId _executingUserId;
        private readonly UserId _storageOwnerId;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresToBeStorageOwnerRule" /> class.
        /// </summary>
        /// <param name="storageOwnerId">Storage owners identifer.</param>
        /// <param name="userContext">Provides informations about the executing user.</param>
        public RequiresToBeStorageOwnerRule(UserId storageOwnerId, IUserContext userContext)
        {
            _executingUserId = userContext.UserId;
            _storageOwnerId = storageOwnerId;
        }

        /// <inheritdoc />
        public string Message => $"The executing user has no permission to write to this storage.";

        /// <inheritdoc />
        public bool Pass() => _executingUserId == _storageOwnerId;
    }
}
