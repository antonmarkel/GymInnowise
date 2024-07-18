using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos;
using GymInnowise.Authorization.Shared.Enums;

namespace GymInnowise.Authorization.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly IRolesRepository _rolesRepo;
        private readonly IJwtService _jwtService;

        public AuthenticationService(IAccountsRepository accountsRepo, IRolesRepository rolesRepo, IJwtService jwtService)
        {
            _accountsRepo = accountsRepo;
            _rolesRepo = rolesRepo;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegistrationRequest accountRegistrationDto)
        {
            if (await _accountsRepo.DoesAccountExistAsync(accountRegistrationDto))
            {
                throw new InvalidOperationException("Account already exists!");
            }

            var account = new AccountEntity
            {
                Email = accountRegistrationDto.Email,
                PhoneNumber = accountRegistrationDto.PhoneNumber,
                PasswordHash = PasswordHelper.HashPassword(accountRegistrationDto.Password),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Roles = [
                    await _rolesRepo.GetRoleAsync(RoleEnum.Client) ??
                        throw new InvalidOperationException("a standard role wasn't found"),
                ],
            };
            await _accountsRepo.CreateAccountAsync(account);
        }

        public async Task<string?> LoginAsync(LoginRequest loginDto)
        {
            var account = await _accountsRepo.GetAccountByEmailAsync(loginDto.Email, loadRoles: true);
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
