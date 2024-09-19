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
    public class CoachProfileService(IProfileRepository<CoachProfileEntity> _coachRepo) : ICoachProfileService
    {
        public async Task<OneOf<Success, ProfileAlreadyExists>> CreateCoachProfileAsync(
            CreateCoachProfileRequest request)
        {
            if (await _coachRepo.DoesProfileExistAsync(request.AccountId))
            {
                return new ProfileAlreadyExists();
            }

            var profileModel = new CoachProfileEntity
            {
                AccountId = request.AccountId,
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

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateCoachProfileAsync(Guid coachId,
            UpdateCoachProfileRequest request)
        {
            var coach = await _coachRepo.GetProfileByIdAsync(coachId);
            if (coach is null)
            {
                return new NotFound();
            }

            coach.FirstName = request.FirstName;
            coach.LastName = request.LastName;
            coach.DateOfBirth = request.DateOfBirth;
            coach.Gender = request.Gender;
            coach.Tags = request.Tags;
            coach.UpdatedAt = DateTime.UtcNow;

            await _coachRepo.UpdateProfileAsync(coach);

            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> UpdateCoachProfileStatusAsync(Guid coachId,
            UpdateCoachProfileStatusRequest request)
        {
            var account = await _coachRepo.GetProfileByIdAsync(coachId);
            if (account is null)
            {
                return new NotFound();
            }

            account.AccountStatus = request.AccountStatus;
            account.StatusNotes = request.StatusNotes;
            account.ExpectedReturnDate = request.ExpectedReturnDate;
            account.UpdatedAt = DateTime.UtcNow;
            account.CoachStatus = request.CoachStatus;

            await _coachRepo.UpdateProfileAsync(account);

            return new Success();
        }

        public async Task<OneOf<GetCoachProfileResponse, NotFound>> GetCoachProfileAsync(Guid id)
        {
            var account = await _coachRepo.GetProfileByIdAsync(id);
            if (account is null)
            {
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
