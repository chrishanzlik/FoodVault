using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Rule for checking the storage name is unique.
    /// </summary>
    public class StorageNameMustBeUniqueRule : IDomainRule
    {
        private readonly IStorageNameUniquessChecker _nameUniquesChecker;
        private readonly string _storageName;
        private readonly UserId _userId;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageNameMustBeUniqueRule" /> class.
        /// </summary>
        /// <param name="storageName">Name to check.</param>
        /// <param name="userId">Users identifier.</param>
        /// <param name="nameUniquessChecker">Domain services that checks a storage name for uniquess.</param>
        public StorageNameMustBeUniqueRule(string storageName, UserId userId, IStorageNameUniquessChecker nameUniquessChecker)
        {
            _storageName = storageName;
            _nameUniquesChecker = nameUniquessChecker;
            _userId = userId;
        }

        /// <inheritdoc />
        public string Message => $"The storage name '{_storageName}' is already forgiven.";

        /// <inheritdoc />
        public bool Pass() => _nameUniquesChecker.IsNameUniqueForUser(_storageName, _userId);
    }
}
