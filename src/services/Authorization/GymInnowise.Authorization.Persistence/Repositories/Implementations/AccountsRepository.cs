using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
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

        public async Task<bool> DoesAccountExistAsync(RegisterRequest registerRequest)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(v =>
                v.PhoneNumber.ToLower() == registerRequest.PhoneNumber.ToLower() ||
                v.Email.ToLower() == registerRequest.Email.ToLower());

            return account != null;
        }

        public async Task UpdateAccountAsync(AccountEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<AccountEntity?> GetAccountByIdAsync(Guid accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(ac => ac.Id == accountId);
        }

        public async Task<AccountEntity?> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(
                a => email.ToLower() == a.Email.ToLower());
        }
    }
}