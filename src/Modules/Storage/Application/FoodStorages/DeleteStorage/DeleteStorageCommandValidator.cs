using FluentValidation;

namespace FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage
{
    /// <summary>
    /// Validator for the <see cref="DeleteStorageCommand"/>.
    /// </summary>
    public class DeleteStorageCommandValidator : AbstractValidator<DeleteStorageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStorageCommandValidator" /> class.
        /// </summary>
        public DeleteStorageCommandValidator()
        {
            RuleFor(x => x.FoodStorageId).NotEmpty();
        }
    }
}
