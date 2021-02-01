using FluentValidation;

namespace FoodVault.Modules.Storage.Application.Products.CreateProduct
{
    /// <summary>
    /// Validator for the <see cref="CreateProductCommand"/>.
    /// </summary>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandValidator" /> class.
        /// </summary>
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
