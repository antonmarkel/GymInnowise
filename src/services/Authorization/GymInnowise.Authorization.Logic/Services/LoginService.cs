using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos;

namespace GymInnowise.Authorization.Logic.Services
{
    public class LoginService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IJwtService _jwtService;

        public LoginService(IAccountsRepository accountsRepository, IJwtService jwtService)
        {
            _accountsRepository = accountsRepository;
            _jwtService = jwtService;
        }

        public async Task<string?> LoginAsync(LoginRequest loginDto)
        {
            var account = await _accountsRepository.GetAccountByEmailAsync(loginDto.Email, loadRoles: true);
            if (account == null)
            {
                throw new InvalidOperationException("account not found");
            }

            if (!PasswordHelper.VerifyPassword(loginDto.Password, account.PasswordHash))
            {
                return null;
            }

            return _jwtService.GenerateJwtToken(account);
        }

    }
}
