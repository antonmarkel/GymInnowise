using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Logic.Handlers.RelationHandlers;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.SectionRelations;
using Moq;

namespace GymInnowise.SectionService.Tests.RelationHandlersTests
{
    public class UpdateSectionRelationHandlerTests
    {
        private readonly Mock<ISectionRelationRepository<SectionGymEntity>> _relationRepository;
        private readonly Mock<IMapper<GymRelation, SectionGymEntity>> _relationMapper;
        private readonly UpdateSectionRelationHandler<SectionGymEntity, GymRelation> _handler;
        private readonly SectionGymEntity _entity;
        private readonly CancellationToken _cancellationToken;
        private readonly IFixture _fixture;

        public UpdateSectionRelationHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _relationRepository = _fixture.Freeze<Mock<ISectionRelationRepository<SectionGymEntity>>>();
            _relationMapper = _fixture.Freeze<Mock<IMapper<GymRelation, SectionGymEntity>>>();
            _handler = _fixture.Create<UpdateSectionRelationHandler<SectionGymEntity, GymRelation>>();
            _cancellationToken = _fixture.Create<CancellationToken>();
            _entity = new SectionGymEntity()
            {
                AddedOnUtc = _fixture.Create<DateTime>(),
                Notes = _fixture.Create<string>(),
                RelatedId = _fixture.Create<Guid>(),
                SectionId = _fixture.Create<Guid>()
            };
        }

        [Fact]
        public async Task UpdateSectionRelationHandle_RelationNotFound_ReturnsNotFound()
        {
            //Arrange
            var relation = _fixture.Create<GymRelation>();
            var request = new UpdateSectionRelationCommand<GymRelation>(relation);
            _relationMapper.Setup(m => m.Map(relation))
                .Returns(_entity);
            _relationRepository.Setup(r => r.ExistsAsync(_entity, _cancellationToken))
                .ReturnsAsync(false);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateSectionRelationHandle_RelationWasFound_ReturnsSuccess()
        {
            //Arrange
            var relation = _fixture.Create<GymRelation>();
            var request = new UpdateSectionRelationCommand<GymRelation>(relation);
            _relationMapper.Setup(m => m.Map(relation))
                .Returns(_entity);
            _relationRepository.Setup(r => r.ExistsAsync(_entity, _cancellationToken))
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT0.Should().BeTrue();
        }
    }
}
