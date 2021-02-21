using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration
{
    /// <summary>
    /// Command handler for <see cref="ConfirmUserRegistrationCommand"/>.
    /// </summary>
    internal class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmUserRegistrationCommandHandler" /> class.
        /// </summary>
        /// <param name="userRegistrationRepository">User registration repository.</param>
        public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ConfirmUserRegistrationCommand command, CancellationToken cancellationToken)
        {
            UserRegistrationId id = new UserRegistrationId(command.UserRegistrationId);

            var registration = await _userRegistrationRepository.GetByIdAsync(id);

            registration?.Confirm();

            return CommandResult.Ok();
        }
    }
}
