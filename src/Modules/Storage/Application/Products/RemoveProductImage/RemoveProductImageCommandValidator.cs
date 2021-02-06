using FluentValidation;

namespace FoodVault.Modules.Storage.Application.Products.RemoveProductImage
{
    /// <summary>
    /// Command validator for <see cref="RemoveProductImageCommand"/>.
    /// </summary>
    internal class RemoveProductImageCommandValidator : AbstractValidator<RemoveProductImageCommand>
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
