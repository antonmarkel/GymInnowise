using GymInnowise.EmailService.Logic.Results;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IEmailService
    {
        Task SendMessageAsync(string receiver, string subject, string message);

        Task<OneOf<Success, NotFound, NotMapped>> SendTemplateMessageAsync(string templateName,
            Dictionary<string, string> model,
            string receiver);
    }
}