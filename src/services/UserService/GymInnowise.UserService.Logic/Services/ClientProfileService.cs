using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using GymInnowise.UserService.Shared.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Services
{
    public class ClientProfileService(IClientProfileRepository _clientRepo) : IClientProfileService
    {
        public async Task CreateClientProfileAsync(CreateClientProfileRequest request)
        {
            if (await _clientRepo.DoesAccountExistAsync(request.AccountId))
            {
                throw new InvalidOperationException("Profile with given accountId already exists!");
            }

            var profileModel = new ClientProfileModel()
            {
                AccountId = request.AccountId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountStatus = ClientStatus.Active,
            };

            await _clientRepo.CreateClientProfileAsync(profileModel);
        }

        public async Task<OneOf<Success, ProfileNotFound>> UpdateClientProfileAsync(UpdateClientProfileRequest request)
        {
            var client = await _clientRepo.GetClientProfileByIdAsync(request.AccountId);
            if (client is null)
            {
                return new ProfileNotFound();
            }

            client.FirstName = request.FirstName;
            client.LastName = request.LastName;
            client.DateOfBirth = request.DateOfBirth;
            client.Gender = request.Gender;
            client.Tags = request.Tags;
            client.UpdatedAt = DateTime.UtcNow;

            await _clientRepo.UpdateClientProfileAsync(client);

            return new Success();
        }

        public async Task<OneOf<Success, ProfileNotFound>> UpdateClientProfileStatusAsync(
            UpdateClientProfileStatusRequest request)
        {
            var account = await _clientRepo.GetClientProfileByIdAsync(request.AccountId);
            if (account is null)
            {
                return new ProfileNotFound();
            }

            account.AccountStatus = request.AccountStatus;
            account.StatusNotes = request.StatusNotes;
            account.ExpectedReturnDate = request.ExpectedReturnDate;
            account.UpdatedAt = DateTime.UtcNow;

            await _clientRepo.UpdateClientProfileAsync(account);

            return new Success();
        }

        public async Task<OneOf<GetClientProfileResponse, ProfileNotFound>> GetClientProfileAsync(Guid id)
        {
            var account = await _clientRepo.GetClientProfileByIdAsync(id);
            if (account is null)
            {
                return new ProfileNotFound();
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

        public async Task<OneOf<Success, ProfileNotFound>> RemoveClientProfileAsync(Guid id)
        {
            if (!await _clientRepo.DoesAccountExistAsync(id))
            {
                return new ProfileNotFound();
            }

            await _clientRepo.RemoveClientProfileAsync(id);

            return new Success();
        }
    }
}
