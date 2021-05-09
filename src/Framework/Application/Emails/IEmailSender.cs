using System.Threading.Tasks;

namespace FoodVault.Framework.Application.Emails
{
    public interface IEmailSender
    {
        Task SendMailAsync(MailMessage message);
    }
}
