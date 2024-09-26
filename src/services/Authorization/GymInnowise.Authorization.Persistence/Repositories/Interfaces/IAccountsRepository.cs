using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task CreateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByEmailAsync(string email);
        Task<AccountEntity?> GetAccountByIdAsync(Guid accountId);
        Task UpdateAccountAsync(AccountEntity entity);
        Task<bool> DoesAccountExistAsync(RegisterRequest registerRequest);
    }
}
