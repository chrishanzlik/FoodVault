using FluentValidation;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser
{
    /// <summary>
    /// Command validator for <see cref="RegisterUserCommand"/>.
    /// </summary>
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommandValidator" /> class.
        /// </summary>
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6); //TODO: Password strength
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
