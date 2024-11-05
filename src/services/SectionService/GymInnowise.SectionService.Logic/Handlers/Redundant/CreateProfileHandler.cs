using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Redundant;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Redundant
{
    public class CreateProfileHandler : IRequestHandler<CreateRedundantCommand<Profile>>
    {
        private readonly IRedundantRepository<ProfileEntity> _profileRepository;
        private readonly IMapper<Profile, ProfileEntity> _profileMapper;

        public CreateProfileHandler(IRedundantRepository<ProfileEntity> redundantRepository,
            IMapper<Profile, ProfileEntity> profileMapper)
        {
            _profileRepository = redundantRepository;
            _profileMapper = profileMapper;
        }

        public async Task Handle(CreateRedundantCommand<Profile> request, CancellationToken cancellationToken)
        {
            var entity = _profileMapper.Map(request.Data);
            await _profileRepository.UploadAsync(entity, cancellationToken);
        }
    }
}
