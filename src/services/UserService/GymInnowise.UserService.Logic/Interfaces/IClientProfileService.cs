﻿using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Interfaces
{
    public interface IClientProfileService
    {
        Task<OneOf<Success, ProfileAlreadyExists>> CreateClientProfileAsync(CreateClientProfileRequest request);

        Task<OneOf<Success, ProfileNotFound>> UpdateClientProfileAsync(Guid clientId,
            UpdateClientProfileRequest request);

        Task<OneOf<Success, ProfileNotFound>> UpdateClientProfileStatusAsync(Guid clientId,
            UpdateClientProfileStatusRequest request);

        Task<OneOf<GetClientProfileResponse, ProfileNotFound>> GetClientProfileAsync(Guid id);
    }
}