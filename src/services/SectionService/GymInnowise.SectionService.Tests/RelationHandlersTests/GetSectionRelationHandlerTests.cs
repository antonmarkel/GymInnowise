using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.SectionService.Logic.Handlers.RelationHandlers;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base.Relations;
using Moq;

namespace GymInnowise.SectionService.Tests.RelationHandlersTests
{
    public class GetSectionRelationHandlerTests
    {
        private readonly Mock<ISectionRelationRepository<SectionCoachEntity>> _relationRepository;
        private readonly CancellationToken _cancellationToken;
        private readonly GetSectionRelationHandler<MentorshipBase, SectionCoachEntity> _handler;
        private readonly SectionCoachEntity _entity;
        private readonly IFixture _fixture;

        public GetSectionRelationHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _cancellationToken = _fixture.Create<CancellationToken>();
            _relationRepository = _fixture.Freeze<Mock<ISectionRelationRepository<SectionCoachEntity>>>();
            _handler = _fixture.Create<GetSectionRelationHandler<MentorshipBase, SectionCoachEntity>>();
            _entity = new SectionCoachEntity
            {
                AddedOnUtc = _fixture.Create<DateTime>(),
                Notes = _fixture.Create<string>(),
                RelatedId = _fixture.Create<Guid>(),
                SectionId = _fixture.Create<Guid>()
            };
        }

        [Fact]
        public async Task GetSectionRelationHandle_RelationNotFound_ReturnsNotFound()
        {
            //Arrange
            var relation = _fixture.Create<MentorshipBase>();
            var request = new GetSectionRelationQuery<MentorshipBase>(relation.SectionId, relation.RelatedId);
            _relationRepository.Setup(r => r.GetAsync(relation.SectionId, relation.RelatedId, _cancellationToken))
                .ReturnsAsync((SectionCoachEntity)null);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task GetSectionRelationHandle_RelationWasFound_ReturnsEntity()
        {
            //Arrange
            var relation = _fixture.Create<MentorshipBase>();
            var request = new GetSectionRelationQuery<MentorshipBase>(relation.SectionId, relation.RelatedId);
            _relationRepository.Setup(r => r.GetAsync(relation.SectionId, relation.RelatedId, _cancellationToken))
                .ReturnsAsync(_entity);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT0.Should().BeTrue();
        }
    }
}
