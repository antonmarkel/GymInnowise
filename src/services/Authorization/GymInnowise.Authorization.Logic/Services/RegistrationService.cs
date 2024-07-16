using GymInnowise.Authorization.Logic.Dtos;
using GymInnowise.Authorization.Persistence.Models;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;


namespace GymInnowise.Authorization.Logic.Services
{
    public class RegistrationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly IRolesRepository _rolesRepo;
        private readonly PasswordService _passwordService;

        public RegistrationService(IAccountsRepository accountsRepo, IRolesRepository rolesRepo, PasswordService passwordService)
        {
            _accountsRepo = accountsRepo;
            _rolesRepo = rolesRepo;
            _passwordService = passwordService;
        }

        //TODO: add a normal validation, not this crap.
        private bool InvalidAccountDto(AccountRegistrationDto accountRegistrationDto)
        {
            return string.IsNullOrEmpty(accountRegistrationDto.PhoneNumber) ||
                string.IsNullOrEmpty(accountRegistrationDto.Email) ||
                string.IsNullOrEmpty(accountRegistrationDto.Password);
        }


        public async Task<bool> RegisterAccount(AccountRegistrationDto accountRegistrationDto)
        {
            if (this.InvalidAccountDto(accountRegistrationDto))
            {

                return false;
            }

            var account = new Account
            {
                Email = accountRegistrationDto.Email,
                PhoneNumber = accountRegistrationDto.PhoneNumber,
                PasswordHash = _passwordService.HashPassword(accountRegistrationDto.Password),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,

                Roles = new List<Role>() {
                    await _rolesRepo.GetRoleAsync("Client"),
                },
            };

            await _accountsRepo.CreateAccountAsync(account);

            return true;
        }
    }
}
