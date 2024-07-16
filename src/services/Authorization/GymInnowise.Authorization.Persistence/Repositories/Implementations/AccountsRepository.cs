using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos;
using GymInnowise.Authorization.Shared.Dtos.Previews;
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

        public async Task CreateAccountAsync(AccountEntity account)
        {     
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesAccountExistAsync(AccountRegistrationRequest dto)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(v =>
                string.Equals(v.PhoneNumber, dto.PhoneNumber, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(v.Email, dto.Email, StringComparison.OrdinalIgnoreCase));
            return account != null;
        }
        public async Task DeleteAccountAsync(AccountEntity account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<AccountEntity?> GetAccountByEmail(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => string.Equals(email, a.Email, StringComparison.OrdinalIgnoreCase));
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
