using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Handlers.Redundant
{
    public class
        CreateRedundantHandler<TRedundant, TRedundantEntity> : IRequestHandler<CreateRedundantCommand<TRedundant>>
        where TRedundant : class, IRedundant
        where TRedundantEntity : class, TRedundant, IEntity
    {
        private readonly IRedundantRepository<TRedundantEntity> _redundantRepository;
        private readonly IMapper<TRedundant, TRedundantEntity> _redundantMapper;
        private readonly ILogger<CreateRedundantHandler<TRedundant, TRedundantEntity>> _logger;

        public CreateRedundantHandler(IRedundantRepository<TRedundantEntity> redundantRepository,
            IMapper<TRedundant, TRedundantEntity> redundantMapper,
            ILogger<CreateRedundantHandler<TRedundant, TRedundantEntity>> logger)
        {
            _redundantRepository = redundantRepository;
            _redundantMapper = redundantMapper;
            _logger = logger;
        }

        public async Task Handle(CreateRedundantCommand<TRedundant> request, CancellationToken cancellationToken)
        {
            var entity = _redundantMapper.Map(request.Data);
            await _redundantRepository.UploadAsync(entity, cancellationToken);
            _logger.LogInformation("Create redundant command was executed. Data: {@data}", request.Data);
        }
    }
}
