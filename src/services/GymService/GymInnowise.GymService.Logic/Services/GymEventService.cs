using AutoMapper;
using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Dtos.Responses.Gets;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Services
{
    public class GymEventService(IGymEventRepository _repo, IMapper _mapper) : IGymEventService
    {
        public async Task CreateGymEventAsync(CreateGymEventRequest request)
        {
            var eventEntity = _mapper.Map<GymEventEntity>(request);
            await _repo.AddEventAsync(eventEntity);
        }

        public async Task<OneOf<Success, NotFound>> UpdateGymEventAsync(Guid eventId, UpdateGymEventRequest request)
        {
            var eventEntity = await _repo.GetGymEventByIdAsync(eventId);
            if (eventEntity is null)
            {
                return new NotFound();
            }

            _mapper.Map(request, eventEntity);
            await _repo.UpdateEventAsync(eventEntity);

            return new Success();
        }

        public async Task<OneOf<Guid, NotFound>> GetGymIdAsync(Guid eventId)
        {
            var eventEntity = await _repo.GetGymEventByIdAsync(eventId);
            if (eventEntity is null)
            {
                return new NotFound();
            }

            return eventEntity.GymId;
        }

        public async Task RemoveGymEventAsync(Guid eventId)
        {
            await _repo.RemoveEventAsync(eventId);
        }

        public async Task<List<GetGymEventResponse>> GetEventsByGymIdAsync(Guid gymId)
        {
            var gymsEvents = await _repo.GetGymEventsByGymIdAsync(gymId);

            return gymsEvents.Select(_mapper.Map<GetGymEventResponse>).ToList();
        }
    }
}