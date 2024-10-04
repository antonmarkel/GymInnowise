using GymInnowise.EmailService.Logic.Results;
using OneOf;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IMessageBuilder
    {
        OneOf<string, NotMapped> BuildMessage(string templateBody, Dictionary<string, string> data);
    }
}
