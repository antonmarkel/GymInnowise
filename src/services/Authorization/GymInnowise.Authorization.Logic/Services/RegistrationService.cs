using GymInnowise.Authorization.Logic.Dtos;
using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;


namespace GymInnowise.Authorization.Logic.Services
{
    public class RegistrationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly IRolesRepository _rolesRepo;

        public RegistrationService(IAccountsRepository accountsRepo, IRolesRepository rolesRepo)
        {
            _accountsRepo = accountsRepo;
            _rolesRepo = rolesRepo;
        }

        //TODO: add a normal validation, not this crap.
        private bool InvalidAccountDto(AccountRegistrationRequest accountRegistrationDto)
        {
            return string.IsNullOrEmpty(accountRegistrationDto.PhoneNumber) ||
                string.IsNullOrEmpty(accountRegistrationDto.Email) ||
                string.IsNullOrEmpty(accountRegistrationDto.Password);
        }


        public async Task<bool> RegisterAccount(AccountRegistrationRequest accountRegistrationDto)
        {
            if (this.InvalidAccountDto(accountRegistrationDto))
            {

                return false;
            }

            var account = new AccountEnity
            {
                Email = accountRegistrationDto.Email,
                PhoneNumber = accountRegistrationDto.PhoneNumber,
                PasswordHash = PasswordHelper.HashPassword(accountRegistrationDto.Password),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,

                Roles = new List<RoleEntity>() {
                    await _rolesRepo.GetRoleAsync("Client") ?? throw new InvalidOperationException("a standard role wasn't found"),
                },
            };

            await _accountsRepo.CreateAccountAsync(account);

            return true;
        }
    }
}
