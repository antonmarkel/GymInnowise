﻿using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Dtos.Responses.Gets;
using GymInnowise.GymService.Shared.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Interfaces
{
    public interface IGymService
    {
        Task CreateGymAsync(CreateGymRequest request);
        Task<OneOf<Success, NotFound>> UpdateGymAsync(Guid gymId, UpdateGymRequest updateRequest);
        Task<OneOf<GetGymDetailsResponse, NotFound>> GetGymDetailsByIdAsync(Guid gymId);
        Task<List<GetGymPreviewResponse>> GetGymPreviewsByTagsAsync(List<GymTag> tags);
    }
}