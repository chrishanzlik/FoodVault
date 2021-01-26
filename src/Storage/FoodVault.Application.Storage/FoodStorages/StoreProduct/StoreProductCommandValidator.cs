using FluentValidation;

namespace FoodVault.Application.Storage.FoodStorages.StoreProduct
{
    /// <summary>
    /// Validator for the <see cref="StoreProductCommand"/>.
    /// </summary>
    public class StoreProductCommandValidator : AbstractValidator<StoreProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProductCommandValidator" /> class.
        /// </summary>
        public StoreProductCommandValidator()
        {
            RuleFor(x => x.StorageId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
