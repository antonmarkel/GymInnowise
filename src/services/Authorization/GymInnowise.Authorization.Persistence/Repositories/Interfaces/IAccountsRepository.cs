using GymInnowise.Authorization.Persistence.Models.Entities;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task CreateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByEmailAsync(string email);
        Task<AccountEntity?> GetAccountByIdAsync(Guid accountId);
        Task UpdateAccountAsync(AccountEntity entity);
        Task UpdateAccountVerificationStatusAsync(Guid accountId, bool isConfirmed = true);
        Task<bool> DoesAccountExistAsync(RegisterRequest registerRequest);
    }
}
