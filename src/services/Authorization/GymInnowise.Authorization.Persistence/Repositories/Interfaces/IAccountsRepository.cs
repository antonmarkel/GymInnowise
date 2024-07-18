using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Dtos;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task CreateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByEmailAsync(string email, bool loadRoles = false);
        Task<bool> DoesAccountExistAsync(RegistrationRequest dto);
    }
}
