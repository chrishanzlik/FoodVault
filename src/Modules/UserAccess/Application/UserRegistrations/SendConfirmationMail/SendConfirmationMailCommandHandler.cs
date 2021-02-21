using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Emails;
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
        private readonly IEmailSender _emailSender;

        public SendConfirmationMailCommandHandler(IUserAccessModuleUrlBuilder userAccessModuleUrlBuilder, IEmailSender emailSender)
        {
            _userAccessModuleUrlBuilder = userAccessModuleUrlBuilder;
            _emailSender = emailSender;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(SendConfirmationMailCommand request, CancellationToken cancellationToken)
        {
            //TODO send mail

            var confirmationLink = _userAccessModuleUrlBuilder.BuildConfirmationLink(request.UserRegistrationId);

            await _emailSender.SendMailAsync(new MailMessage(request.Email.Value, "YOUR REGISTRATION", confirmationLink));

            return CommandResult.Ok();
        }
    }
}
