using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using GymInnowise.UserService.Shared.Enums;
using OneOf.Types;
using OneOf;

namespace GymInnowise.UserService.Logic.Services
{
    public class ClientProfileService(IClientProfileRepository _clientRepo) : IClientProfileService
    {
        public async Task CreateClientProfileAsync(CreateClientProfileRequest request)
        {
            if (await _clientRepo.DoesAccountExistAsync(request.AccountId))
            {
                throw new InvalidOperationException("Profile with given accountId already Exist!");
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

        public async Task<OneOf<Success, AccountNotFound>> UpdateClientProfileAsync(UpdateClientProfileRequest request)
        {
            var account = await _clientRepo.GetClientProfileByIdAsync(request.AccountId);
            if (account is null)
            {
                return new AccountNotFound();
            }

            account.FirstName = request.FirstName;
            account.LastName = request.LastName;
            account.DateOfBirth = request.DateOfBirth;
            account.Gender = request.Gender;
            account.Tags = request.Tags;
            account.UpdatedAt = DateTime.UtcNow;

            await _clientRepo.UpdateClientProfileAsync(account);

            return new Success();
        }

        public async Task<OneOf<Success, AccountNotFound>> UpdateProfileStatusAsync(
            UpdateClientProfileStatusRequest request)
        {
            var account = await _clientRepo.GetClientProfileByIdAsync(request.AccountId);
            if (account is null)
            {
                return new AccountNotFound();
            }

            account.AccountStatus = request.AccountStatus;
            account.StatusNotes = request.StatusNotes;
            account.ExpectedReturnDate = request.ExpectedReturnDate;
            account.UpdatedAt = DateTime.UtcNow;

            await _clientRepo.UpdateClientProfileAsync(account);

            return new Success();
        }

        public async Task<GetClientProfileRequest?> GetClientProfileAsync(Guid id)
        {
            var account = await _clientRepo.GetClientProfileByIdAsync(id);

            return new GetClientProfileRequest()
            {
                FirstName = account.FirstName,
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

        public async Task<OneOf<Success, AccountNotFound>> RemoveClientProfileAsync(Guid id)
        {
            if (!await _clientRepo.DoesAccountExistAsync(id))
            {
                return new AccountNotFound();
            }

            await _clientRepo.RemoveClientProfileAsync(id);

            return new Success();
        }
    }
}
