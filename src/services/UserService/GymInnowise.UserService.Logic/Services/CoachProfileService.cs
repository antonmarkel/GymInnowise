﻿using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels;
using GymInnowise.UserService.Shared.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Services
{
    public class CoachProfileService(ICoachProfileRepository _coachRepo) : ICoachProfileService
    {
        public async Task CreateClientProfileAsync(CreateCoachProfileRequest request)
        {
            if (await _coachRepo.DoesAccountExistAsync(request.AccountId))
            {
                throw new InvalidOperationException("Profile with given accountId already Exist!");
            }

            var profileModel = new CoachProfileModel()
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
                CoachStatus = CoachStatus.Trial,
            };

            await _coachRepo.CreateCoachProfileAsync(profileModel);
        }

        public async Task<OneOf<Success, AccountNotFound>> UpdateClientProfileAsync(UpdateCoachProfileRequest request)
        {
            var coach = await _coachRepo.GetCoachProfileByIdAsync(request.AccountId);
            if (coach is null)
            {
                return new AccountNotFound();
            }

            coach.FirstName = request.FirstName;
            coach.LastName = request.LastName;
            coach.DateOfBirth = request.DateOfBirth;
            coach.Gender = request.Gender;
            coach.Tags = request.Tags;
            coach.UpdatedAt = DateTime.UtcNow;

            await _coachRepo.UpdateCoachProfileAsync(coach);

            return new Success();
        }

        public async Task<OneOf<Success, AccountNotFound>> UpdateProfileStatusAsync(
            UpdateCoachProfileStatusRequest request)
        {
            var account = await _coachRepo.GetCoachProfileByIdAsync(request.AccountId);
            if (account is null)
            {
                return new AccountNotFound();
            }

            account.AccountStatus = request.AccountStatus;
            account.StatusNotes = request.StatusNotes;
            account.ExpectedReturnDate = request.ExpectedReturnDate;
            account.UpdatedAt = DateTime.UtcNow;
            account.CoachStatus = request.CoachStatus;

            await _coachRepo.UpdateCoachProfileAsync(account);

            return new Success();
        }

        public async Task<OneOf<GetCoachProfileResponse, AccountNotFound>> GetCoachProfileAsync(Guid id)
        {
            var account = await _coachRepo.GetCoachProfileByIdAsync(id);
            if (account is null)
            {
                return new AccountNotFound();
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

        public async Task<OneOf<Success, AccountNotFound>> RemoveClientProfileAsync(Guid id)
        {
            if (!await _coachRepo.DoesAccountExistAsync(id))
            {
                return new AccountNotFound();
            }

            await _coachRepo.RemoveCoachProfileAsync(id);

            return new Success();
        }
    }
}