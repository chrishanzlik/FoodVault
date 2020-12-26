using FluentValidation;

namespace FoodVault.Application.Storage.FoodStorages.RemoveStorage
{
    /// <summary>
    /// Validator for the <see cref="RemoveStorageCommand"/>.
    /// </summary>
    public class RemoveStorageCommandValidator : AbstractValidator<RemoveStorageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveStorageCommandValidator" /> class.
        /// </summary>
        public RemoveStorageCommandValidator()
        {
            RuleFor(x => x.FoodStorageId).NotEmpty();
        }
    }
}
