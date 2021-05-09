using FluentValidation;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ChangeStorageProfile
{
    /// <summary>
    /// Validator for the <see cref="ChangeStorageProfileCommand"/>.
    /// </summary>
    internal class ChangeStorageProfileCommandValidator : AbstractValidator<ChangeStorageProfileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeStorageProfileCommandValidator" /> class.
        /// </summary>
        public ChangeStorageProfileCommandValidator()
        {
            RuleFor(x => x.StorageName).NotEmpty().MinimumLength(4);
        }
    }
}
