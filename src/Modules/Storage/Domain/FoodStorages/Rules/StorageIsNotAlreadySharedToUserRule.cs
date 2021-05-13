using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Ensures that a storage is not already shared to a user.
    /// </summary>
    public class StorageIsNotAlreadySharedToUserRule : IDomainRule
    {
        private readonly UserId _userId;
        private readonly IEnumerable<StorageShare> _activeShares;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageIsNotAlreadySharedToUserRule" /> class.
        /// </summary>
        /// <param name="userId">Users identifer.</param>
        /// <param name="activeShares">All active shares of the storage.</param>
        public StorageIsNotAlreadySharedToUserRule(UserId userId, IEnumerable<StorageShare> activeShares)
        {
            _userId = userId;
            _activeShares = activeShares;
        }

        public string Message => $"The storage is already shared to the user '{_userId}'";

        public bool Pass() => !_activeShares.Any(x => x.UserId == _userId);
    }
}
