using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task CreateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByEmailAsync(string email);
        Task<bool> DoesAccountExistAsync(RegisterRequest dto);
    }
}
