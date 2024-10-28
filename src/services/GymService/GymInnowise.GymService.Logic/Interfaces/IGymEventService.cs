using GymInnowise.Shared.Gym.Dtos.Requests.Creates;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;
using GymInnowise.Shared.Gym.Dtos.Responses.Gets;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Interfaces
{
    public interface IGymEventService
    {
        Task<Guid> CreateGymEventAsync(CreateGymEventDtoRequest dtoRequest);
        Task<OneOf<Success, NotFound>> UpdateGymEventAsync(Guid eventId, UpdateGymEventDtoRequest dtoRequest);
        Task<OneOf<GetGymEventResponse, NotFound>> GetEventByIdAsync(Guid eventId);
        Task RemoveGymEventAsync(Guid eventId);
        Task<OneOf<Guid, NotFound>> GetGymIdAsync(Guid eventId);
        Task<IEnumerable<GetGymEventResponse>> GetEventsByGymIdAsync(Guid gymId);
    }
}