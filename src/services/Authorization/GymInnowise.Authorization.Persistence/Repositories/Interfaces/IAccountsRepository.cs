using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Models.Previews;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task<bool> CreateAccountAsync(AccountEnity account);
        Task DeleteAccountAsync(AccountEnity account);
        Task<AccountEnity?> GetAccountByEmail(string email);
        Task<IEnumerable<AccountPreview>> GetAllAccountsAsync();
    }
}
