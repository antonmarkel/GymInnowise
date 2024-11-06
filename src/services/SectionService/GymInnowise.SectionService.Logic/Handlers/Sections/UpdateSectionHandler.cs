using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class UpdateSectionHandler : IRequestHandler<UpdateSectionCommand, OneOf<Success, NotFound>>
    {
        private readonly ISectionRepository _sectionRepository;

        public UpdateSectionHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<OneOf<Success, NotFound>> Handle(UpdateSectionCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _sectionRepository.ExistsByIdAsync(request.SectionId, cancellationToken))
            {
                return new NotFound();
            }

            await _sectionRepository.UpdateSectionByIdAsync(request.SectionId, request.UpdateData, cancellationToken);

            return new Success();
        }
    }
}
