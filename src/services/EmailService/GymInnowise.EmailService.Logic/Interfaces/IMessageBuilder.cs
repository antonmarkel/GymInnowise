using GymInnowise.Shared.Email.Messages;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IMessageBuilder
    {
        Task<OneOf<Message, NotFound>> BuildMessageFromTemplateAsync(TemplateMessage templateMessage);
    }
}