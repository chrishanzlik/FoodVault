using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Ensures that there are enaugh items to remove.
    /// </summary>
    public class HasEnaughProductQuantityToRemove : IDomainRule
    {
        private readonly int _actualQuantity;
        private readonly int _remove;

        /// <summary>
        /// Initializes a new instance of the <see cref="HasEnaughProductQuantityToRemove" /> class.
        /// </summary>
        /// <param name="actualQuantity">Actual storage quantity of the product.</param>
        /// <param name="remove">How many products should be removed.</param>
        public HasEnaughProductQuantityToRemove(int actualQuantity, int remove)
        {
            _actualQuantity = actualQuantity;
            _remove = remove;
        }

        /// <inheritdoc />
        public string Message => $"Cannot remove {_remove} items. There are only {_actualQuantity} items left.";

        /// <inheritdoc />
        public bool Validate() => _actualQuantity >= _remove;
    }
}
