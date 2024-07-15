using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Repositories.Implementations
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AuthorizationDbContext _context;
        public AccountsRepository(AuthorizationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAccountAsync(Account account)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.Email == account.Email || a.PhoneNumber == account.PhoneNumber);
            if (existAccount != null) return false;
            
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAccountAsync(Guid id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if (account is null) return false;
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email) ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            return await _context.Accounts.Include(a => a.Roles).Select(a => new AccountDto{ 
                Id = a.Id, Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Roles = a.Roles.Select(ar => ar.RoleName).ToArray()
            }).ToListAsync();
        }

    }
}
