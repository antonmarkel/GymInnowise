using GymInnowise.Authorization.Configuration;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Results;
using GymInnowise.Authorization.Persistence.Models.Entities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Events;
using GymInnowise.Shared.Email.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly VerificationSettings _settings;
        private readonly IPublishEndpoint _publisher;
        private readonly IVerificationRepository _repo;
        private readonly IAccountsRepository _accountsRepo;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public VerificationService(IVerificationRepository repo,
            IOptions<VerificationSettings> settings, IPublishEndpoint publisher,
            IAccountsRepository accountsRepo, IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
        {
            _repo = repo;
            _settings = settings.Value;
            _publisher = publisher;
            _accountsRepo = accountsRepo;
            _contextAccessor = contextAccessor;
            _linkGenerator = linkGenerator;
        }

        public async Task<OneOf<Success, NotFound, Expired>> VerifyAsync(Guid verificationId)
        {
            var verification = await _repo.GetVerificationAsync(verificationId);
            if (verification is null)
            {
                return new NotFound();
            }

            await _repo.RemoveVerificationAsync(verification.Id);
            if (verification.ExpireAt < DateTime.UtcNow)
            {
                return new Expired();
            }

            await _accountsRepo.UpdateAccountVerificationStatusAsync(verification.Id);

            return new Success();
        }

        public async void StartVerificationAsync(Guid accountId, string email)
        {
            var emailVerification = new VerificationEntity()
            {
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddMinutes(_settings.ExpireAfterMinutes),
            };
            await _repo.CreateVerificationAsync(emailVerification);

            var verificationToken = emailVerification.Id;
            var verifyLink = _linkGenerator.GetUriByName(_contextAccessor.HttpContext!,
                IVerificationService.VerificationActionName, values: new { verificationToken });

            var messageEvent = new SendMessageEvent()
            {
                EmailMessage = new Message()
                {
                    Body = $"Confirm your email, following the link:{verifyLink}",
                    Receiver = email,
                    Subject = "Email confirmation"
                }
            };
            await _publisher.Publish(messageEvent);
        }
    }
}