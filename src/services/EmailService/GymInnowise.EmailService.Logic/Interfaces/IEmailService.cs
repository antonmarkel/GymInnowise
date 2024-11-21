using GymInnowise.Shared.Email.Messages;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IEmailService
    {
        Task SendMessageAsync(Message message);
        Task<OneOf<Success, NotFound>> SendTemplateMessageAsync(TemplateMessage templateMessage);
    }
}