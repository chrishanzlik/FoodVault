using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Rule for checking the storage name is unique.
    /// </summary>
    public class StorageNameMustBeUniqueRule : IDomainRule
    {
        private readonly IStorageNameUniquessChecker _nameUniquesChecker;
        private readonly string _storageName;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageNameMustBeUniqueRule" /> class.
        /// </summary>
        /// <param name="storageName">Name to check.</param>
        /// <param name="nameUniquessChecker">Domain services that checks a storage name for uniquess.</param>
        public StorageNameMustBeUniqueRule(string storageName, IStorageNameUniquessChecker nameUniquessChecker)
        {
            _storageName = storageName;
            _nameUniquesChecker = nameUniquessChecker;
        }

        /// <inheritdoc />
        public string Message => $"The storage name '{_storageName}' is already forgiven.";

        /// <inheritdoc />
        public bool Pass() => _nameUniquesChecker.StorageNameIsUnique(_storageName);
    }
}
