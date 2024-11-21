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
        UpdateRedundantHandler<TRedundant, TRedundantEntity> : IRequestHandler<UpdateRedundantCommand<TRedundant>>
        where TRedundant : class, IRedundant
        where TRedundantEntity : class, TRedundant, IEntity
    {
        private readonly IRedundantRepository<TRedundantEntity> _redundantRepository;
        private readonly IMapper<TRedundant, TRedundantEntity> _redundantMapper;
        private readonly ILogger<UpdateRedundantHandler<TRedundant, TRedundantEntity>> _logger;

        public UpdateRedundantHandler(IRedundantRepository<TRedundantEntity> redundantRepository,
            IMapper<TRedundant, TRedundantEntity> redundantMapper,
            ILogger<UpdateRedundantHandler<TRedundant, TRedundantEntity>> logger)
        {
            _redundantRepository = redundantRepository;
            _redundantMapper = redundantMapper;
            _logger = logger;
        }

        public async Task Handle(UpdateRedundantCommand<TRedundant> request, CancellationToken cancellationToken)
        {
            var entity = _redundantMapper.Map(request.UpdateData);
            await _redundantRepository.UpdateAsync(entity, cancellationToken);
            _logger.LogInformation("Update redundant command was executed. Data: {@Data}", request.UpdateData);
        }
    }
}
