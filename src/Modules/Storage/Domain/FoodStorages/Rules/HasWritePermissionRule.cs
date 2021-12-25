using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Ensures that the executing user has write permissions.
    /// </summary>
    public class HasWritePermissionRule : IDomainRule
    {
        private readonly UserId _executingUserId;
        private readonly UserId _storageOwnerId;
        private readonly IEnumerable<StorageShare> _shares;

        /// <summary>
        /// Initializes a new instance of the <see cref="HasWritePermissionRule" /> class.
        /// </summary>
        /// <param name="storageOwnerId">Storage owners identifer.</param>
        /// <param name="shares">All applicable storage shares.</param>
        /// <param name="userContext">Provides informations about the executing user.</param>
        public HasWritePermissionRule(UserId storageOwnerId, IEnumerable<StorageShare> shares, IUserContext userContext)
        {
            _executingUserId = userContext.UserId;
            _storageOwnerId = storageOwnerId;
            _shares = shares;
        }

        /// <inheritdoc />
        public string Message => $"The executing user has no permission to write to this storage.";

        /// <inheritdoc />
        public bool Pass() =>
            _executingUserId == _storageOwnerId
            || _shares.Any(share => share.IsWritePermissionEnabled && share.UserId == _executingUserId);
    }
}
