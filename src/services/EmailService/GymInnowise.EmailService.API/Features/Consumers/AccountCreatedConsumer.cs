using GymInnowise.EmailService.API.Services.Interfaces;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Events;
using MassTransit;

namespace GymInnowise.EmailService.Logic.Features.Accounts
{
    public sealed class AccountCreatedConsumer : IConsumer<AccountCreatedEvent>
    {
        private readonly ILinkFactory _linkFactory;
        private readonly IVerificationService _verificationService;

        public AccountCreatedConsumer(ILinkFactory linkFactory, IVerificationService verificationService)
        {
            _linkFactory = linkFactory;
            _verificationService = verificationService;
        }

        public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
        {
            var verificationToken =
                await _verificationService.CreateVerificationToken(context.Message.Email, context.Message.AccountId);
            var link = await _linkFactory.GenerateVerificationLink(verificationToken);
            await _verificationService.SendVerificationAsync(context.Message.Email, link);
        }
    }
}
