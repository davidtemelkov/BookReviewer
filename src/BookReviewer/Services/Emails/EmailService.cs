namespace BookReviewer.Services.Emails
{
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;


    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var apiKey = this.configuration.GetValue<string>("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(this.configuration.GetValue<string>("SenderEmail"), "BookReviewer");
            var recipient = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, recipient, subject,string.Empty, body);
            await client.SendEmailAsync(msg);
        }
    }
}
