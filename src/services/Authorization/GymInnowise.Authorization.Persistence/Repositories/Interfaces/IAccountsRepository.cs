

using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Dtos.Previews;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task<bool> CreateAccountAsync(AccountEntity account);
        Task DeleteAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByEmail(string email);
        Task<IEnumerable<AccountPreview>> GetAllAccountsAsync();
    }
}
