using AutoMapper;
using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Dtos.Responses.Gets;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Services
{
    public class GymEventService(IGymEventRepository _repo, IMapper _mapper, ILogger<GymEventService> _logger)
        : IGymEventService
    {
        public async Task CreateGymEventAsync(CreateGymEventRequest request)
        {
            var eventEntity = _mapper.Map<GymEventEntity>(request);
            _logger.LogInformation("Gym event was created @{eventEntity}");
            await _repo.AddEventAsync(eventEntity);
        }

        public async Task<OneOf<Success, NotFound>> UpdateGymEventAsync(Guid eventId, UpdateGymEventRequest request)
        {
            var eventEntity = await _repo.GetGymEventByIdAsync(eventId);
            if (eventEntity is null)
            {
                _logger.LogInformation("Error while updating gym event. Reason: event with id {@eventId} wasn't found",
                    eventId);

                return new NotFound();
            }

            _mapper.Map(request, eventEntity);
            await _repo.UpdateEventAsync(eventEntity);
            _logger.LogInformation("Gym event was successfully updated. Info: {@eventId} {@reguest}", eventId, request);

            return new Success();
        }

        public async Task<OneOf<Guid, NotFound>> GetGymIdAsync(Guid eventId)
        {
            var eventEntity = await _repo.GetGymEventByIdAsync(eventId);
            if (eventEntity is null)
            {
                _logger.LogInformation("Gym event with id {@eventId} was not found", eventId);

                return new NotFound();
            }

            return eventEntity.GymId;
        }

        public async Task RemoveGymEventAsync(Guid eventId)
        {
            _logger.LogInformation("event with id {@eventId} was deleted", eventId);
            await _repo.RemoveEventAsync(eventId);
        }

        public async Task<List<GetGymEventResponse>> GetEventsByGymIdAsync(Guid gymId)
        {
            var gymsEvents = await _repo.GetGymEventsByGymIdAsync(gymId);

            return gymsEvents.Select(_mapper.Map<GetGymEventResponse>).ToList();
        }
    }
}