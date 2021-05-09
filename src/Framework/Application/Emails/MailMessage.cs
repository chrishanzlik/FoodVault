namespace FoodVault.Framework.Application.Emails
{
    public struct MailMessage
    {
        public MailMessage(
            string to,
            string subject,
            string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }

        public string To { get; }

        public string Subject { get; }

        public string Content { get; }
    }
}
