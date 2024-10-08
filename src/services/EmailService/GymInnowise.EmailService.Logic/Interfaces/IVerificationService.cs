using GymInnowise.EmailService.Logic.Results;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IVerificationService
    {
        Task<OneOf<Success, NotFound, Expired>> VerifyTokenAsync(Guid token);
        Task StartVerificationAsync(string email, string link);
    }
}
