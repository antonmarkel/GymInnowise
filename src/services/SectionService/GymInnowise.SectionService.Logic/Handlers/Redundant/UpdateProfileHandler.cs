using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Redundant;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Redundant
{
    public class UpdateProfileHandler : IRequestHandler<UpdateRedundantCommand<Profile>>
    {
        private readonly IRedundantRepository<ProfileEntity> _profileRepository;
        private readonly IMapper<Profile, ProfileEntity> _profileMapper;

        public UpdateProfileHandler(IRedundantRepository<ProfileEntity> profileRepository,
            IMapper<Profile, ProfileEntity> profileMapper)
        {
            _profileRepository = profileRepository;
            _profileMapper = profileMapper;
        }

        public async Task Handle(UpdateRedundantCommand<Profile> request, CancellationToken cancellationToken)
        {
            var entity = _profileMapper.Map(request.UpdateData);
            await _profileRepository.UpdateByIdAsync(entity, cancellationToken);
        }
    }
}
