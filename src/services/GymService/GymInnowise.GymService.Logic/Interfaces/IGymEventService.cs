using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Dtos.Responses.Gets;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Interfaces
{
    public interface IGymEventService
    {
        Task CreateGymEventAsync(CreateGymEventRequest request);
        Task<OneOf<Success, NotFound>> UpdateGymEventAsync(Guid eventId, UpdateGymEventRequest request);
        Task RemoveGymEventAsync(Guid eventId);
        Task<OneOf<Guid, NotFound>> GetGymIdAsync(Guid eventId);
        Task<List<GetGymEventResponse>> GetEventsByGymIdAsync(Guid gymId);
    }
}
