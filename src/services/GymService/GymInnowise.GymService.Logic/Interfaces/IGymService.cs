using GymInnowise.GymService.Logic.Results;
using GymInnowise.GymService.Shared.Dtos.Requests;
using GymInnowise.GymService.Shared.Dtos.Responses;
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
        Task<OneOf<List<GetGymPreviewResponse>, TagsEmpty>> GetGymPreviewsByTagsAsync(List<GymTag> tags);
    }
}
