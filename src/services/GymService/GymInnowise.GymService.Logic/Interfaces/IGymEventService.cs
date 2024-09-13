using GymInnowise.GymService.Shared.Dtos.Requests;
using GymInnowise.GymService.Shared.Dtos.Responses;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Interfaces
{
    public interface IGymEventService
    {
        Task CreateGymEventAsync(CreateGymEventRequest request);
        Task<OneOf<Success, NotFound>> UpdateGymEventAsync(Guid eventId, UpdateGymEventRequest request);
        Task RemoveGymEventAsync(Guid eventId);
        Task<List<GetGymEventResponse>> GetEventsByGymIdAsync(Guid gymId);
    }
}
