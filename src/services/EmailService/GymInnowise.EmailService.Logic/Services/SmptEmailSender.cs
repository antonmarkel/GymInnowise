using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Logic.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GymInnowise.EmailService.Logic.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<EmailSettings> emailSettings, ILogger<SmtpEmailSender> logger)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("No reply", _emailSettings.FromAddress));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            _logger.LogInformation("Connected to smtp server.");
            await client.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            _logger.LogInformation("Smtp user was authenticated.");
            await client.SendAsync(emailMessage);
            _logger.LogInformation("Smtp client sent email message");
            await client.DisconnectAsync(true);
            _logger.LogInformation("Disconnected from smtp server");
        }
    }
}