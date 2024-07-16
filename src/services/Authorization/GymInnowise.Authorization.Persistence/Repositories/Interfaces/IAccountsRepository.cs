

using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Dtos;
using GymInnowise.Authorization.Shared.Dtos.Previews;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task CreateAccountAsync(AccountEntity account);
        Task DeleteAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByEmail(string email);
        Task<IEnumerable<AccountPreview>> GetAllAccountsAsync();
        Task<bool> DoesAccountExist(AccountRegistrationRequest dto)
    }
}
