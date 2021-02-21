using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations.Events;
using FoodVault.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration
{
    internal class UserRegistrationConfirmedHandler : INotificationHandler<UserRegistrationConfirmedEvent>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUserRepository _userRepository;

        public UserRegistrationConfirmedHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUserRepository userRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(UserRegistrationConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var registration = await _userRegistrationRepository.GetByIdAsync(notification.UserRegistrationId);

            var user = registration.CreateUser();

            await _userRepository.AddAsync(user);
        }
    }
}
