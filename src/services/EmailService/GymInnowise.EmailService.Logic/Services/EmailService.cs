using GymInnowise.EmailService.Logic.Interfaces;

namespace GymInnowise.EmailService.Logic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendMessageAsync(string receiver, string subject, string message)
        {
            await _emailSender.SendEmailAsync(receiver, subject, message);
        }
    }
}
