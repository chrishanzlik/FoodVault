using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.UserAccess.Application.Authentication;
using FoodVault.Modules.UserAccess.Domain;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser
{
    /// <summary>
    /// Command handler for <see cref="RegisterUserCommand"/>.
    /// </summary>
    internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IEmailFreeChecker _emailFreeChecker;
        private readonly IPasswordManager _passwordManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommandHandler" /> class.
        /// </summary>
        /// <param name="userRegistrationRepository">User registration repository.</param>
        /// <param name="emailFreeChecker">Checks if an email is free for registration.</param>
        /// <param name="passwordManager">Utility for hashing passwords.</param>
        public RegisterUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IEmailFreeChecker emailFreeChecker,
            IPasswordManager passwordManager)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _emailFreeChecker = emailFreeChecker;
            _passwordManager = passwordManager;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var hashString = _passwordManager.HashPassword(command.Password);
            var hash = new PasswordHash(hashString);

            var registration = UserRegistration.RegisterUser(
                new EmailAddress(command.Email),
                hash,
                command.FirstName,
                command.LastName,
                _emailFreeChecker);

            await _userRegistrationRepository.AddAsync(registration);

            return CommandResult.EntityCreated(registration.Id);
        }
    }
}
