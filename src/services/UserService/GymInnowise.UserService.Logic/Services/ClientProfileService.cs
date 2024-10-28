using GymInnowise.Shared.User.Dtos.RequestModels.Creates;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.Shared.User.Dtos.ResponseModels.Gets;
using GymInnowise.Shared.User.Enums;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Services
{
    public class ClientProfileService(
        IProfileRepository<ClientProfileEntity> _clientRepo,
        ILogger<ClientProfileService> _logger) : IClientProfileService
    {
        public async Task<OneOf<Success, ProfileAlreadyExists>> CreateClientProfileAsync(Guid accountId,
            CreateClientProfileRequest request)
        {
            if (await _clientRepo.DoesProfileExistAsync(accountId))
            {
                _logger.LogWarning(
                    "Client profile wasn't created. Reason: profile with this accountId {@accountId} already exists!",
                    accountId);

                return new ProfileAlreadyExists();
            }

            var profileModel = new ClientProfileEntity
            {
                AccountId = accountId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountStatus = ClientStatus.Active
            };

            await _clientRepo.CreateProfileAsync(profileModel);
            _logger.LogInformation(
                "Client profile was created successfully. Info: {@accountId}",
                accountId);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateClientProfileAsync(Guid accountId,
            UpdateClientProfileRequest request)
        {
            var client = await _clientRepo.GetProfileByIdAsync(accountId);
            if (client is null)
            {
                _logger.LogWarning(
                    "Client profile wasn't updated. Reason: profile with this accountId {@accountId} was not found!",
                    accountId);

                return new NotFound();
            }

            client.FirstName = request.FirstName;
            client.LastName = request.LastName;
            client.DateOfBirth = request.DateOfBirth;
            client.Gender = request.Gender;
            client.Tags = request.Tags;
            client.UpdatedAt = DateTime.UtcNow;

            await _clientRepo.UpdateProfileAsync(client);
            _logger.LogInformation(
                "Client profile was updated successfully. Info: {@accountId}",
                accountId);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateClientProfileStatusAsync(Guid accountId,
            UpdateClientProfileStatusRequest request)
        {
            var account = await _clientRepo.GetProfileByIdAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning(
                    "Client profile wasn't updated. Reason: profile with this accountId {@accountId} was not found!",
                    accountId);

                return new NotFound();
            }

            account.AccountStatus = request.AccountStatus;
            account.StatusNotes = request.StatusNotes;
            account.ExpectedReturnDate = request.ExpectedReturnDate;
            account.UpdatedAt = DateTime.UtcNow;

            await _clientRepo.UpdateProfileAsync(account);
            _logger.LogInformation(
                "Client profile was updated successfully. Info: {@accountId}",
                accountId);

            return new Success();
        }

        public async Task<OneOf<GetClientProfileResponse, NotFound>> GetClientProfileAsync(Guid accountId)
        {
            var account = await _clientRepo.GetProfileByIdAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning(
                    "Client profile with this id: {@accountId} was not found!",
                    accountId);

                return new NotFound();
            }

            return new GetClientProfileResponse()
            {
                FirstName = account!.FirstName,
                LastName = account.LastName,
                DateOfBirth = account.DateOfBirth,
                Gender = account.Gender,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt,
                AccountStatus = account.AccountStatus,
                StatusNotes = account.StatusNotes,
                ExpectedReturnDate = account.ExpectedReturnDate,
                Tags = account.Tags
            };
        }
    }
}