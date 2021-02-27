using FluentValidation;

namespace FoodVault.Modules.UserAccess.Application.Authentication.Authenticate
{
    /// <summary>
    /// Validator for the <see cref="AuthenticateCommand"/>.
    /// </summary>
    internal class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateCommandValidator" /> class.
        /// </summary>
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
