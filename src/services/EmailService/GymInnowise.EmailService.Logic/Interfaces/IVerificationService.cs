using GymInnowise.EmailService.Logic.Results;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IVerificationService
    {
        Task<OneOf<Success, NotFound, Expired>> VerifyAsync(Guid token);
        Task<Guid> CreateVerificationToken(string email, Guid accountId);
        Task SendVerificationAsync(string email, string link);
    }
}
