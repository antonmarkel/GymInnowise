using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class UpdateSectionHandler : IRequestHandler<UpdateSectionCommand>
    {
        private readonly ISectionRepository _sectionRepository;

        public UpdateSectionHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            await _sectionRepository.UpdateSectionByIdAsync(request.SectionId, request.UpdateData, cancellationToken);
        }
    }
}
