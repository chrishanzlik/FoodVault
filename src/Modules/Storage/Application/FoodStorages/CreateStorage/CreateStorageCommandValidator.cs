using FluentValidation;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    /// <summary>
    /// Validator for the <see cref="CreateStorageCommand"/>.
    /// </summary>
    internal class CreateStorageCommandValidator : AbstractValidator<CreateStorageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommandValidator" /> class.
        /// </summary>
        public CreateStorageCommandValidator()
        {
            RuleFor(x => x.StorageName).NotEmpty().MinimumLength(4);
        }
    }
}
