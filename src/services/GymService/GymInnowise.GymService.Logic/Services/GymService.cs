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
    public class GymService : IGymService
    {
        private readonly IGymRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GymService> _logger;

        public GymService(IGymRepository repo, IMapper mapper, ILogger<GymService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> CreateGymAsync(CreateGymRequest request)
        {
            var gymEntity = _mapper.Map<GymEntity>(request);
            await _repo.AddGymAsync(gymEntity);
            _logger.LogInformation("Gym was created. Info: {@gymEntity}", gymEntity);

            return gymEntity.Id;
        }

        public async Task<OneOf<Success, NotFound>> UpdateGymAsync(Guid gymId, UpdateGymRequest updateRequest)
        {
            var gymEntity = await _repo.GetGymByIdAsync(gymId);
            if (gymEntity is null)
            {
                _logger.LogWarning("Error while updating. Reason: gym with id {@gymId} was not found!", gymId);

                return new NotFound();
            }

            _mapper.Map(updateRequest, gymEntity);
            await _repo.UpdateGymAsync(gymEntity);
            _logger.LogInformation("Gym was successfully updated. Info: {@gymId}", gymId);

            return new Success();
        }

        public async Task<OneOf<GetGymDetailsResponse, NotFound>> GetGymDetailsByIdAsync(Guid gymId)
        {
            var gymEntity = await _repo.GetGymByIdAsync(gymId);
            if (gymEntity is null)
            {
                _logger.LogWarning("Gym with id {@gymId} was not found.", gymId);

                return new NotFound();
            }

            return _mapper.Map<GetGymDetailsResponse>(gymEntity);
        }

        public async Task<IEnumerable<GetGymPreviewResponse>> GetGymPreviewsByTagsAsync(IEnumerable<string> tags)
        {
            var gymPreviewDtos = await _repo.GetGymsByTagsAsync(tags);

            return gymPreviewDtos.Select(_mapper.Map<GetGymPreviewResponse>).ToList();
        }
    }
}