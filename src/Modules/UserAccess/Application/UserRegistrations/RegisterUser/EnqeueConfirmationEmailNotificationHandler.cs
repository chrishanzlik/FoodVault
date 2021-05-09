using FoodVault.Framework.Application;
using FoodVault.Modules.UserAccess.Application.UserRegistrations.SendConfirmationMail;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser
{
    /// <summary>
    /// Enqeues a command that is responsible for sending the confirmation link to the user.
    /// </summary>
    internal class EnqeueConfirmationEmailNotificationHandler : INotificationHandler<UserRegisteredNotification>
    {
        private readonly ICommandScheduler _commandScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnqeueConfirmationEmailNotificationHandler" /> class.
        /// </summary>
        /// <param name="commandScheduler">Schedules commands for execution outside of the transaction scope.</param>
        public EnqeueConfirmationEmailNotificationHandler(ICommandScheduler commandScheduler)
        {
            _commandScheduler = commandScheduler;
        }

        /// <inheritdoc />
        public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            var command = new SendConfirmationMailCommand(
                Guid.NewGuid(),
                notification.DomainEvent.UserRegistrationId,
                notification.DomainEvent.Email);

            await _commandScheduler.ScheduleAsync(command);
        }
    }
}
