using AutoMapper;
using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Gym.Dtos.Requests.Creates;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;
using GymInnowise.Shared.Gym.Dtos.Responses.Gets;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Services
{
    public class GymEventService : IGymEventService
    {
        private readonly IGymEventRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GymEventService> _logger;

        public GymEventService(IGymEventRepository repo, IMapper mapper, ILogger<GymEventService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> CreateGymEventAsync(CreateGymEventDtoRequest dtoRequest)
        {
            var eventEntity = _mapper.Map<GymEventEntity>(dtoRequest);
            _logger.LogInformation("Gym event was created @{eventEntity}", eventEntity);
            await _repo.AddEventAsync(eventEntity);

            return eventEntity.Id;
        }

        public async Task<OneOf<Success, NotFound>> UpdateGymEventAsync(Guid eventId,
            UpdateGymEventDtoRequest dtoRequest)
        {
            var eventEntity = await _repo.GetGymEventByIdAsync(eventId);
            if (eventEntity is null)
            {
                _logger.LogWarning("Error while updating gym event. Reason: event with id {@eventId} wasn't found",
                    eventId);

                return new NotFound();
            }

            _mapper.Map(dtoRequest, eventEntity);
            await _repo.UpdateEventAsync(eventEntity);
            _logger.LogInformation("Gym event was successfully updated. Info: {@eventId} {@reguest}", eventId,
                dtoRequest);

            return new Success();
        }

        public async Task<OneOf<Guid, NotFound>> GetGymIdAsync(Guid eventId)
        {
            var eventEntity = await _repo.GetGymEventByIdAsync(eventId);
            if (eventEntity is null)
            {
                _logger.LogWarning("Gym event with id {@eventId} was not found", eventId);

                return new NotFound();
            }

            return eventEntity.GymId;
        }

        public async Task RemoveGymEventAsync(Guid eventId)
        {
            _logger.LogInformation("event with id {@eventId} was deleted", eventId);
            await _repo.RemoveEventAsync(eventId);
        }

        public async Task<IEnumerable<GetGymEventResponse>> GetEventsByGymIdAsync(Guid gymId)
        {
            var gymsEvents = await _repo.GetGymEventsByGymIdAsync(gymId);

            return gymsEvents.Select(_mapper.Map<GetGymEventResponse>).ToList();
        }

        public async Task<OneOf<GetGymEventResponse, NotFound>> GetEventByIdAsync(Guid eventId)
        {
            var entity = await _repo.GetGymEventByIdAsync(eventId);
            if (entity is null)
            {
                _logger.LogWarning("Gym event with id {@eventId} was not found", eventId);

                return new NotFound();
            }

            return _mapper.Map<GetGymEventResponse>(entity);
        }
    }
}