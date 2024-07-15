using GymInnowise.Authorization.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
