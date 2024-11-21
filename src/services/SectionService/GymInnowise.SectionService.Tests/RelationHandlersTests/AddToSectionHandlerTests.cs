using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Logic.Handlers.RelationHandlers;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base.Relations;
using Moq;

namespace GymInnowise.SectionService.Tests.RelationHandlersTests
{
    public class AddToSectionHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ISectionRelationRepository<SectionMemberEntity>> _relationRepository;
        private readonly Mock<ISectionRepository> _sectionRepository;
        private readonly Mock<IRedundantRepository<ProfileEntity>> _entityRepository;
        private readonly Mock<IMapper<MembershipBase, SectionMemberEntity>> _relationMapper;
        private readonly AddToSectionHandler<SectionMemberEntity, ProfileEntity, MembershipBase> _handler;
        private readonly SectionMemberEntity _entity;
        private readonly CancellationToken _cancellationToken;

        public AddToSectionHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _relationRepository = _fixture.Freeze<Mock<ISectionRelationRepository<SectionMemberEntity>>>();
            _sectionRepository = _fixture.Freeze<Mock<ISectionRepository>>();
            _entityRepository = _fixture.Freeze<Mock<IRedundantRepository<ProfileEntity>>>();
            _relationMapper = _fixture.Freeze<Mock<IMapper<MembershipBase, SectionMemberEntity>>>();
            _entity = new SectionMemberEntity
            {
                AddedOnUtc = _fixture.Create<DateTime>(),
                Goal = _fixture.Create<string>(),
                RelatedId = _fixture.Create<Guid>(),
                SectionId = _fixture.Create<Guid>()
            };
            _cancellationToken = _fixture.Create<CancellationToken>();

            _handler = _fixture.Create<AddToSectionHandler<SectionMemberEntity, ProfileEntity, MembershipBase>>();
        }

        [Fact]
        public async Task AddToSectionHandle_RelatedNotFound_ReturnsNotFound()
        {
            //Arrange
            var relation = _fixture.Create<MembershipBase>();
            var request = new AddToSectionCommand<MembershipBase>(relation);
            _entityRepository.Setup(
                    r => r.ExistsByIdAsync(relation.RelatedId, _cancellationToken))
                .ReturnsAsync(false);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task AddToSectionHandle_SectionNotFound_ReturnsNotFound()
        {
            //Arrange
            var relation = _fixture.Create<MembershipBase>();
            var request = new AddToSectionCommand<MembershipBase>(relation);
            _sectionRepository.Setup(
                    r => r.ExistsByIdAsync(relation.RelatedId, _cancellationToken))
                .ReturnsAsync(false);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task AddToSectionHandle_RelationNotFound_ReturnsNotFound()
        {
            //Arrange
            var relation = _fixture.Create<MembershipBase>();
            var request = new AddToSectionCommand<MembershipBase>(relation);
            _relationMapper.Setup(m => m.Map(relation)).Returns(_entity);
            _relationRepository.Setup(
                    r => r.ExistsAsync(_entity, _cancellationToken))
                .ReturnsAsync(false);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task AddToSectionHandle_AllFound_ReturnsSuccess()
        {
            //Arrange
            var relation = _fixture.Create<MembershipBase>();
            var request = new AddToSectionCommand<MembershipBase>(relation);

            _relationMapper.Setup(m => m.Map(relation)).Returns(_entity);
            _sectionRepository.Setup(
                    r => r.ExistsByIdAsync(relation.SectionId, _cancellationToken))
                .ReturnsAsync(true);
            _entityRepository.Setup(
                    r => r.ExistsByIdAsync(relation.RelatedId, _cancellationToken))
                .ReturnsAsync(true);
            _relationRepository.Setup(
                    r => r.ExistsAsync(_entity, _cancellationToken))
                .ReturnsAsync(false);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT0.Should().BeTrue();
        }
    }
}
