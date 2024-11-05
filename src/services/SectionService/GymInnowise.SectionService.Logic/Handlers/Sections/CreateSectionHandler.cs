using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class CreateSectionHandler : IRequestHandler<CreateSectionCommand>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper<SectionBase, SectionEntity> _sectionMapper;

        public CreateSectionHandler(ISectionRepository sectionRepository,
            IMapper<SectionBase, SectionEntity> sectionMapper)
        {
            _sectionRepository = sectionRepository;
            _sectionMapper = sectionMapper;
        }

        public async Task Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = _sectionMapper.Map(request.SectionData);
            await _sectionRepository.CreateSectionAsync(entity, cancellationToken);
        }
    }
}
