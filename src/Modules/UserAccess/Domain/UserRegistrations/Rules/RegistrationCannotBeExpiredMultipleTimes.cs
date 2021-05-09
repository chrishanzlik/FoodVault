using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    /// <summary>
    /// Rule that checks that a registration cannot be expired multiple times.
    /// </summary>
    internal class RegistrationCannotBeExpiredMultipleTimesRule : IDomainRule
    {
        private readonly RegistrationState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationCannotBeExpiredMultipleTimesRule" /> class.
        /// </summary>
        /// <param name="state">Current state of the registration.</param>
        public RegistrationCannotBeExpiredMultipleTimesRule(RegistrationState state)
        {
            _state = state;
        }

        /// <inheritdoc />
        public string Message => "Registration cannot be expired multiple times.";

        /// <inheritdoc />
        public bool Pass() => _state != RegistrationState.Expired;
    }
}
