using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Results;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using OneOf.Types;
using OneOf;

namespace GymInnowise.EmailService.Logic.Services
{
    public class VerificationService(IEmailVerificationRepository _repo) : IVerificationService
    {
        public async Task<OneOf<Success, NotFound, Expired>> VerifyAsync(Guid token)
        {
            var verification = await _repo.GetVerificationAsync(token);
            if (verification is null)
            {
                return new NotFound();
            }

            if (verification.ExpireAt < DateTime.UtcNow)
            {
                return new Expired();
            }

            return new Success();
        }
    }
}
