using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Results;
using GymInnowise.Authorization.Persistence.Models.Entities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;
using GymInnowise.Shared.RabbitMq.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly ILogger<AccountService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IVerificationService _verificationService;

        public AccountService(IAccountsRepository accountsRepo, IPublishEndpoint publishEndpoint,
            ILogger<AccountService> logger, IVerificationService verificationService)
        {
            _accountsRepo = accountsRepo;
            _logger = logger;
            _verificationService = verificationService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<OneOf<Success, AccountAlreadyExists>> RegisterAsync(
            RegisterRequest registerRequest)
        {
            if (await _accountsRepo.DoesAccountExistAsync(registerRequest))
            {
                _logger.LogWarning("Registration failed. Reason: 'Account already exists' Email:'{@Email}'.",
                    registerRequest.Email);

                return new AccountAlreadyExists();
            }

            var account = new AccountEntity
            {
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = PasswordHelper.HashPassword(registerRequest.Password),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Roles = [Roles.Client],
            };
            await _accountsRepo.CreateAccountAsync(account);
            _logger.LogInformation("New account with email '{Email}' has been created.", account.Email);

            var accountCreatedEvent = new AccountCreatedEvent { AccountId = account.Id, Email = account.Email };
            await _publishEndpoint.Publish(accountCreatedEvent);

            _verificationService.StartVerificationAsync(account.Id, account.Email);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateAsync(Guid accountId,
            UpdateRequest updateRequest)
        {
            var account = await _accountsRepo.GetAccountByIdAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning("Updating failed, no account was found {accountId}", accountId);

                return new NotFound();
            }

            account.Email = updateRequest.Email;
            account.PhoneNumber = updateRequest.PhoneNumber;
            account.PasswordHash = PasswordHelper.HashPassword(updateRequest.Password);
            await _accountsRepo.UpdateAccountAsync(account);
            _logger.LogInformation("Account updated '{accountId}'.", accountId);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateRolesAsync(Guid accountId,
            UpdateRolesRequest updateRolesRequest)
        {
            var account = await _accountsRepo.GetAccountByIdAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning("Updating failed, no account was found {accountId}", accountId);

                return new NotFound();
            }

            account.Roles = updateRolesRequest.Roles.ToList();
            await _accountsRepo.UpdateAccountAsync(account);
            _logger.LogInformation("Account updated '{accountId}'.", accountId);

            return new Success();
        }
    }
}