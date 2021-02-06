using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Rule for checking the storage name for its length.
    /// </summary>
    public class StorageNameMustBeMinFourCharactersLongRule : IDomainRule
    {
        private readonly string _storageName;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageNameMustBeMinFourCharactersLongRule" /> class.
        /// </summary>
        /// <param name="storageName">Name to check.</param>
        public StorageNameMustBeMinFourCharactersLongRule(string storageName)
        {
            _storageName = storageName;
        }

        /// <inheritdoc />
        public string Message => "The strage name must be at least 4 characters long.";

        /// <inheritdoc />
        public bool Validate()
        {
            return _storageName != null && _storageName.Length >= 4;
        }
    }
}
