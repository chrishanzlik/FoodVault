namespace FoodVault.Domain.Storage.FoodStore.Rules
{
    /// <summary>
    /// Rule for checking that a product operation has a valid quantity.
    /// </summary>
    public sealed class ProductOperationHasValidQuantityRule : IDomainRule
    {
        private readonly int _quantity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOperationHasValidQuantityRule" /> class.
        /// </summary>
        /// <param name="quantity">Quantity to check.</param>
        public ProductOperationHasValidQuantityRule(int quantity)
        {
            _quantity = quantity;
        }

        /// <inheritdoc />
        public string Message => "Quantity of a added or removed product must be at least 1.";

        /// <inheritdoc />
        public bool Validate()
        {
            return _quantity >= 1;
        }
    }
}
