using FluentValidation;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration
{
    /// <summary>
    /// Command validator for <see cref="ConfirmUserRegistrationCommand"/>.
    /// </summary>
    internal class ConfirmUserRegistrationCommandValidator : AbstractValidator<ConfirmUserRegistrationCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmUserRegistrationCommandValidator" /> class.
        /// </summary>
        public ConfirmUserRegistrationCommandValidator()
        {
            RuleFor(x => x.UserRegistrationId).NotEmpty();
        }
    }
}
