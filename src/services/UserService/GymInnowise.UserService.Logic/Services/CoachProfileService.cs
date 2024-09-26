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
    public class CoachProfileService(
        IProfileRepository<CoachProfileEntity> _coachRepo,
        ILogger<CoachProfileService> _logger) : ICoachProfileService
    {
        public async Task<OneOf<Success, ProfileAlreadyExists>> CreateCoachProfileAsync(Guid accountId,
            CreateCoachProfileRequest request)
        {
            if (await _coachRepo.DoesProfileExistAsync(accountId))
            {
                _logger.LogWarning(
                    "Coach profile wasn't created. Reason: profile with this accountId {@accountId} already exists!",
                    accountId);

                return new ProfileAlreadyExists();
            }

            var profileModel = new CoachProfileEntity
            {
                AccountId = accountId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                HiredAt = DateTime.UtcNow,
                AccountStatus = ClientStatus.Active,
                CostPerHour = request.CostPerHour,
                CoachStatus = CoachStatus.Trial
            };

            await _coachRepo.CreateProfileAsync(profileModel);
            _logger.LogInformation(
                "Coach profile was created successfully. Info: {@accountId}",
                accountId);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateCoachProfileAsync(Guid accountId,
            UpdateCoachProfileRequest request)
        {
            var coach = await _coachRepo.GetProfileByIdAsync(accountId);
            if (coach is null)
            {
                _logger.LogWarning(
                    "Coach profile wasn't updated. Reason: profile with this accountId {@accountId} was not found!",
                    accountId);

                return new NotFound();
            }

            coach.FirstName = request.FirstName;
            coach.LastName = request.LastName;
            coach.DateOfBirth = request.DateOfBirth;
            coach.Gender = request.Gender;
            coach.Tags = request.Tags;
            coach.UpdatedAt = DateTime.UtcNow;

            await _coachRepo.UpdateProfileAsync(coach);
            _logger.LogInformation(
                "Coach profile was updated successfully. Info: {@accountId}",
                accountId);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateCoachProfileStatusAsync(Guid accountId,
            UpdateCoachProfileStatusRequest request)
        {
            var account = await _coachRepo.GetProfileByIdAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning(
                    "Coach profile wasn't updated. Reason: profile with this accountId {@accountId} was not found!",
                    accountId);

                return new NotFound();
            }

            account.AccountStatus = request.AccountStatus;
            account.StatusNotes = request.StatusNotes;
            account.ExpectedReturnDate = request.ExpectedReturnDate;
            account.UpdatedAt = DateTime.UtcNow;
            account.CoachStatus = request.CoachStatus;

            await _coachRepo.UpdateProfileAsync(account);
            _logger.LogInformation(
                "Coach profile was updated successfully. Info: {@accountId}",
                accountId);

            return new Success();
        }

        public async Task<OneOf<GetCoachProfileResponse, NotFound>> GetCoachProfileAsync(Guid accountId)
        {
            var account = await _coachRepo.GetProfileByIdAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning(
                    "Coach profile with this id: {@accountId} was not found!",
                    accountId);

                return new NotFound();
            }

            return new GetCoachProfileResponse()
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
                Tags = account.Tags,
                HiredAt = account.HiredAt,
                CostPerHour = account.CostPerHour,
                CoachStatus = account.CoachStatus
            };
        }
    }
}