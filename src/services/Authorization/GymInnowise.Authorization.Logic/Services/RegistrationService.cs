using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos;
using GymInnowise.Authorization.Shared.Enums;


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

        public async Task RegisterAccountAsync(RegistrationRequest accountRegistrationDto)
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
                Roles = new List<RoleEntity>() {
                    await _rolesRepo.GetRoleAsync(RoleEnum.Client) ??
                        throw new InvalidOperationException("a standard role wasn't found"),
                },
            };
            await _accountsRepo.CreateAccountAsync(account);
        }
    }
}
