using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class GetSectionPreviewHandler : IRequestHandler<GetSectionPreviewQuery, OneOf<SectionBase, NotFound>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<GetSectionPreviewHandler> _logger;

        public GetSectionPreviewHandler(ISectionRepository sectionRepository, ILogger<GetSectionPreviewHandler> logger)
        {
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        public async Task<OneOf<SectionBase, NotFound>> Handle(GetSectionPreviewQuery request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _sectionRepository.GetSectionPreviewByIdAsync(request.SectionId, asNoTracking: true,
                    cancellationToken);
            if (entity is null)
            {
                _logger.LogWarning("Section was not found {sectionId}!", request.SectionId);

                return new NotFound();
            }

            _logger.LogInformation("Section information was successfully retrieved {sectionId}.",
                request.SectionId);

            return entity;
        }
    }
}
