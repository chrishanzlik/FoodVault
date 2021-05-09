using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    /// <summary>
    /// Rule that ensures, that a given email is unique in the system.
    /// </summary>
    internal class UserEmailNotInUseRule : IDomainRule
    {
        private readonly IEmailFreeChecker _emailFreeChecker;
        private readonly EmailAddress _email;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEmailNotInUseRule" /> class.
        /// </summary>
        /// <param name="emailFreeChecker">Domain services that checks, that a email is unique and not in use.</param>
        /// <param name="email">Email to register.</param>
        public UserEmailNotInUseRule(IEmailFreeChecker emailFreeChecker, EmailAddress email)
        {
            _emailFreeChecker = emailFreeChecker;
            _email = email;
        }

        /// <inheritdoc />
        public string Message => $"The E-Mail address {_email.Value} is already registered.";
        
        /// <inheritdoc />
        public bool Pass() => _emailFreeChecker.IsFreeEmail(_email.Value);
    }
}
