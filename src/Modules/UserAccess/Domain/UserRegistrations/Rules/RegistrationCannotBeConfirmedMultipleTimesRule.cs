using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    /// <summary>
    /// Rule that checks that a registration cannot be confirmed multiple times.
    /// </summary>
    internal class RegistrationCannotBeConfirmedMultipleTimesRule : IDomainRule
    {
        private readonly RegistrationState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationCannotBeConfirmedMultipleTimesRule" /> class.
        /// </summary>
        /// <param name="state">Current state of the registration.</param>
        public RegistrationCannotBeConfirmedMultipleTimesRule(RegistrationState state)
        {
            _state = state;
        }

        /// <inheritdoc />
        public string Message => "Registration cannot be confirmed multiple times.";

        /// <inheritdoc />
        public bool Pass() => _state != RegistrationState.Confirmed;
    }
}
