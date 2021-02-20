using FluentValidation;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage
{
    /// <summary>
    /// Command validator for <see cref="ShareStorageCommand"/>.
    /// </summary>
    internal class ShareStorageCommandValidator : AbstractValidator<ShareStorageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShareStorageCommandValidator" /> class.
        /// </summary>
        public ShareStorageCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FoodStorageId).NotEmpty();
        }
    }
}
