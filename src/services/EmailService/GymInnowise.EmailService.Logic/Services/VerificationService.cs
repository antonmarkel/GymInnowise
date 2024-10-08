using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Results;
using GymInnowise.EmailService.Persistence.Dto;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Configuration;
using GymInnowise.EmailService.Shared.Dtos.Events;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Services
{
    public class VerificationService(
        IEmailVerificationRepository _repo,
        IEmailService _emailService,
        IOptions<VerificationSettings> _settings,
        IPublishEndpoint _publisher)
        : IVerificationService
    {
        public async Task<OneOf<Success, NotFound, Expired>> VerifyTokenAsync(Guid token)
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

            await _publisher.Publish(new AccountVerifiedEvent()
            {
                AccountId = verification.AccountId
            });
            await _repo.RemoveVerificationAsync(verification);

            return new Success();
        }

        public async Task<Guid> StartVerificationAsync(string email, Guid accountId)
        {
            var createEmailVerification = new CreateEmailVerification()
            {
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddMinutes(_settings.Value.ExpireAfterMinutes)
            };
            await _repo.CreateVerificationAsync(createEmailVerification);

            return createEmailVerification.Id;
        }

        public async Task SendVerificationAsync(string email, string link)
        {
            var subject = "Confirm your email!";
            var message = $"To confirm your email follow the link: {link}";
            await _emailService.SendMessageAsync(email, subject, message);
        }
    }
}
