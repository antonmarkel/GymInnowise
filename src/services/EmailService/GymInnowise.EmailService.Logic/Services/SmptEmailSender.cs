using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Logic.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GymInnowise.EmailService.Logic.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public SmtpEmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer);
            smtpClient.Port = _emailSettings.SmtpPort;
            smtpClient.Credentials =
                new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            smtpClient.EnableSsl = true;
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}