using GymInnowise.EmailService.Logic.Helpers;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Results;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRepository _repo;
        private readonly IMessageBuilder _messageBuilder;

        public EmailService(IEmailSender emailSender, ITemplateRepository repo,
            IMessageBuilder messageBuilder)
        {
            _emailSender = emailSender;
            _repo = repo;
            _messageBuilder = messageBuilder;
        }

        public async Task SendMessageAsync(string receiver, string subject, string message)
        {
            await _emailSender.SendEmailAsync(receiver, subject, message);
        }

        public async Task<OneOf<Success, NotFound, NotMapped>> SendTemplateMessageAsync(string templateName,
            Dictionary<string, string> model,
            string receiver)
        {
            var template = await _repo.GetTemplateAsync(templateName);
            if (template is null)
            {
                return new NotFound();
            }

            if (!TemplateVerifier.VerifyTemplateBinding(model, template.Data))
            {
                return new NotMapped();
            }

            var messageBuildResult = _messageBuilder.BuildMessage(template.Body, model);
            if (messageBuildResult.IsT1)
            {
                return new NotMapped();
            }

            var message = messageBuildResult.AsT0;
            await SendMessageAsync(receiver, template.Subject, message);

            return new Success();
        }
    }
}
