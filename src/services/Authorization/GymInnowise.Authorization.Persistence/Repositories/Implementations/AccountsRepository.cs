using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos;
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

        public async Task<bool> DoesAccountExistAsync(RegistrationRequest dto)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(v =>
                v.PhoneNumber.ToLower() == dto.PhoneNumber.ToLower() ||
                v.Email.ToLower() == dto.Email.ToLower());

            return account != null;
        }

        public async Task<AccountEntity?> GetAccountByEmailAsync(string email, bool loadRoles = false)
        {
            var query = _context.Accounts.AsQueryable();
            if (loadRoles)
            {
                query = query.Include(a => a.Roles);
            }

            return await query.FirstOrDefaultAsync(a => email.ToLower() == a.Email.ToLower());
        }
    }
}
