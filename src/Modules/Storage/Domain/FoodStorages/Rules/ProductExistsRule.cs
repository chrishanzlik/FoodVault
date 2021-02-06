using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    /// <summary>
    /// Rule that checks if a product exists.
    /// </summary>
    public class ProductExistsRule : IDomainRule
    {
        private readonly IProductExistsChecker _productExistsChecker;
        private readonly ProductId _productId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductExistsRule" /> class.
        /// </summary>
        /// <param name="productId">Products id.</param>
        /// <param name="productExistsChecker">Domain service that checks if a product exists.</param>
        public ProductExistsRule(ProductId productId, IProductExistsChecker productExistsChecker)
        {
            _productId = productId;
            _productExistsChecker = productExistsChecker;
        }

        /// <inheritdoc />
        public string Message => $"A product with the id '{_productId}' does not exists.";

        /// <inheritdoc />
        public bool Validate() => _productExistsChecker.ProductExists(_productId);
    }
}
