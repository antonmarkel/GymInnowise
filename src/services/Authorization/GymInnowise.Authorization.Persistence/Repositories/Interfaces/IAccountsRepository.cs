using GymInnowise.Authorization.Persistence.Models;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task<bool> CreateAccountAsync(Account account);

        Task<bool> DeleteAccountAsync(Guid id);

        Task<Account> GetAccountByEmail(string email);

        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
    }
}
