using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.UserAccess.Application.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.SendConfirmationMail
{
    /// <summary>
    /// Command handler for the <see cref="SendConfirmationMailCommand"/>.
    /// </summary>
    internal class SendConfirmationMailCommandHandler : ICommandHandler<SendConfirmationMailCommand>
    {
        private readonly IUserAccessModuleUrlBuilder _userAccessModuleUrlBuilder;

        /// <inheritdoc />
        public Task<ICommandResult> Handle(SendConfirmationMailCommand request, CancellationToken cancellationToken)
        {
            //TODO send mail

            var confirmationLink = _userAccessModuleUrlBuilder.BuildConfirmationLink(request.UserRegistrationId);

            return Task.FromResult<ICommandResult>(CommandResult.Ok());
        }
    }
}
