using GymInnowise.Shared.Gym.Dtos.Requests.Creates;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;
using GymInnowise.Shared.Gym.Dtos.Responses.Gets;
using GymInnowise.Shared.Gym.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Interfaces
{
    public interface IGymService
    {
        Task<Guid> CreateGymAsync(CreateGymRequest request);
        Task<OneOf<Success, NotFound>> UpdateGymAsync(Guid gymId, UpdateGymRequest updateRequest);
        Task<OneOf<GetGymDetailsResponse, NotFound>> GetGymDetailsByIdAsync(Guid gymId);
        Task<IEnumerable<GetGymPreviewResponse>> GetGymPreviewsByTagsAsync(IEnumerable<GymTag> tags);
    }
}