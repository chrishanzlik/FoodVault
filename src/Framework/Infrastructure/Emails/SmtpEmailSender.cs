using FoodVault.Framework.Application.Emails;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure.Emails
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IMailerSettings _settings;

        public SmtpEmailSender(IMailerSettings settings)
        {
            _settings = settings;
        }

        public Task SendMailAsync(MailMessage message)
        {
            //TODO:
            Debug.WriteLine($"SENDING DUMMY MAIL TO {message.To}...");

            return Task.CompletedTask;
        }
    }
}
