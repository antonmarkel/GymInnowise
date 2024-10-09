using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.EmailService.Shared.Dtos.Events;
using MassTransit;

namespace GymInnowise.Authorization.API.Features.Consumers
{
    public class AccountVerifiedConsumer : IConsumer<AccountVerifiedEvent>
    {
        private readonly IAccountsRepository _repo;

        public AccountVerifiedConsumer(IAccountsRepository repo)
        {
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<AccountVerifiedEvent> context)
        {
            var account = await _repo.GetAccountByIdAsync(context.Message.AccountId);
            if (account is null)
            {
                return;
            }

            account.IsEmailConfirmed = true;
            await _repo.UpdateAccountAsync(account);
        }
    }
}
