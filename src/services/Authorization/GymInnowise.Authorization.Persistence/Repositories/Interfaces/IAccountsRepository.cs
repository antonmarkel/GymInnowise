using GymInnowise.Authorization.Persistence.Models;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task<bool> CreateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
        Task<Account?> GetAccountByEmail(string email);
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
    }
}
