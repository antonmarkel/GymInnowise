using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Persistence.Repositories
{
    public class AccountsRepository
    {
        private readonly AuthorizationDbContext _context;
        public AccountsRepository(AuthorizationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAccountAsync(Guid id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if (account is null) return false;
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

       
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

    }
}
