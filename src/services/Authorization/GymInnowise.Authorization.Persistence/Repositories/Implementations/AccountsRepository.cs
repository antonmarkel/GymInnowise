using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Models.Previews;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.Authorization.Persistence.Repositories.Implementations
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AuthorizationDbContext _context;

        public AccountsRepository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAccountAsync(AccountEnity account)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.Email == account.Email || a.PhoneNumber == account.PhoneNumber);
            if (existAccount != null)
            {

                return false;
            }

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAccountAsync(AccountEnity account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<AccountEnity?> GetAccountByEmail(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<AccountPreview>> GetAllAccountsAsync()
        {
            return await _context.Accounts.Include(a => a.Roles)
                .Select(a =>
                    new AccountPreview
                    {
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        Roles = a.Roles.Select(ar => ar.RoleName).ToArray()
                    }
                ).ToListAsync();
        }

    }
}
