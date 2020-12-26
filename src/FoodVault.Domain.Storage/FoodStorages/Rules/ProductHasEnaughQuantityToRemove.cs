namespace FoodVault.Domain.Storage.FoodStorages.Rules
{
    /// <summary>
    /// Rule for checking that a product has enaugh items to remove.
    /// </summary>
    public class ProductHasEnaughQuantityToRemove : IDomainRule
    {
        private readonly int _actualQuantity;
        private readonly int _quantityToRemove;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductHasEnaughQuantityToRemove" /> class.
        /// </summary>
        /// <param name="actualQuantity">Actual products quantity.</param>
        /// <param name="quantityToRemove">Quantity to manipulate.</param>
        public ProductHasEnaughQuantityToRemove(int actualQuantity, int quantityToRemove)
        {
            _actualQuantity = actualQuantity;
            _quantityToRemove = quantityToRemove;
        }

        /// <inheritdoc />
        public string Message => $"The product has not enaugh quantity ({_quantityToRemove}) to remove {_quantityToRemove} items.";

        /// <inheritdoc />
        public bool Validate() => _actualQuantity >= _quantityToRemove;
    }
}
