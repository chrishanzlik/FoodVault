using FluentValidation;

namespace FoodVault.Modules.Storage.Application.FoodStorages.UnshareStorage
{
    /// <summary>
    /// Command validator for <see cref="UnshareStorageCommandValidator"/>.
    /// </summary>
    internal class UnshareStorageCommandValidator : AbstractValidator<UnshareStorageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnshareStorageCommandValidator" /> class.
        /// </summary>
        public UnshareStorageCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FoodStorageId).NotEmpty();
        }
    }
}
