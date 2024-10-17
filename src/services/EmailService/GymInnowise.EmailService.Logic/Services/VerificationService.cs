using GymInnowise.EmailService.Configuration.Email;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Logic.Results;
using GymInnowise.EmailService.Persistence.Models;
using GymInnowise.EmailService.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Events;
using MassTransit;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;

namespace GymInnowise.EmailService.Logic.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IEmailVerificationRepository _repo;
        private readonly IEmailService _emailService;
        private readonly VerificationSettings _settings;
        private readonly IPublishEndpoint _publisher;

        public VerificationService(IEmailVerificationRepository repo, IEmailService emailService,
            IOptions<VerificationSettings> settings, IPublishEndpoint publisher)
        {
            _repo = repo;
            _emailService = emailService;
            _settings = settings.Value;
            _publisher = publisher;
        }


        public async Task<OneOf<Success, NotFound, Expired>> VerifyAsync(Guid token)
        {
            var verification = await _repo.GetVerificationAsync(token);
            if (verification is null)
            {
                return new NotFound();
            }

            if (verification.ExpireAt < DateTime.UtcNow)
            {
                await _repo.RemoveVerificationAsync(verification);

                return new Expired();
            }

            await _publisher.Publish(new AccountVerifiedEvent
            {
                AccountId = verification.AccountId
            });
            await _repo.RemoveVerificationAsync(verification);

            return new Success();
        }

        public async Task<Guid> CreateVerificationTokenAsync(Guid accountId)
        {
            var createEmailVerification = new EmailVerificationEntity()
            {
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddMinutes(_settings.ExpireAfterMinutes)
            };
            await _repo.CreateVerificationAsync(createEmailVerification);

            return createEmailVerification.Id;
        }

        public async Task SendVerificationAsync(string email, string link)
        {
            //TODO: SEND TEMPLATED MESSAGE
            var subject = "Confirm your email!";
            var message = $"To confirm your email follow the link: {link}";
            await _emailService.SendMessageAsync(email, subject, message);
        }
    }
}
