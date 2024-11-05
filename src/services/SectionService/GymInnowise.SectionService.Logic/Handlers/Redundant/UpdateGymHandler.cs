using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Redundant;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Redundant
{
    public class UpdateGymHandler : IRequestHandler<UpdateRedundantCommand<Gym>>
    {
        private readonly IRedundantRepository<GymEntity> _gymRepository;
        private readonly IMapper<Gym, GymEntity> _gymMapper;

        public UpdateGymHandler(IRedundantRepository<GymEntity> gymRepository, IMapper<Gym, GymEntity> gymMapper)
        {
            _gymRepository = gymRepository;
            _gymMapper = gymMapper;
        }

        public async Task Handle(UpdateRedundantCommand<Gym> request, CancellationToken cancellationToken)
        {
            var entity = _gymMapper.Map(request.UpdateData);
            await _gymRepository.UploadAsync(entity, cancellationToken);
        }
    }
}
