using FluentValidation;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// Command validator for <see cref="RemoveProductImageCommand"/>.
    /// </summary>
    public class RemoveProductImageCommandValidator : AbstractValidator<RemoveProductImageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProductImageCommandValidator" /> class.
        /// </summary>
        public RemoveProductImageCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
