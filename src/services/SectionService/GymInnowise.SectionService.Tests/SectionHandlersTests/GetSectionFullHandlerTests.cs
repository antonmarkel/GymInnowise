using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.SectionService.Logic.Handlers.Sections;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using Moq;

namespace GymInnowise.SectionService.Tests.SectionHandlersTests
{
    public class GetSectionFullHandlerTests
    {
        private readonly Mock<ISectionRepository> _sectionRepository;
        private readonly CancellationToken _cancellationToken;
        private readonly GetSectionFullHandler _handler;
        private readonly IFixture _fixture;

        public GetSectionFullHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _sectionRepository = _fixture.Freeze<Mock<ISectionRepository>>();
            _cancellationToken = _fixture.Freeze<CancellationToken>();
            _handler = _fixture.Create<GetSectionFullHandler>();
        }

        [Fact]
        public async Task GetSectionFullHandle_SectionNotFound_ReturnsNotFound()
        {
            //Arrange
            var request = _fixture.Create<GetSectionFullQuery>();
            _sectionRepository.Setup(r => r.GetSectionIncludeReferencesByIdAsync(request.SectionId, _cancellationToken))
                .ReturnsAsync((SectionEntity?)null);
            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task GetSectionFullHandle_SectionWasFound_ReturnsSectionFullResponse()
        {
            //Arrange
            var sectionId = _fixture.Create<Guid>();
            var request = new GetSectionFullQuery(sectionId);
            var mockEntity = new SectionEntity
            {
                Coaches = [],
                Members = [],
                Gyms = [],
                CostPerTraining = _fixture.Create<decimal>(),
                Description = _fixture.Create<string>(),
                IsActive = true,
                Name = _fixture.Create<string>(),
                PrimaryId = sectionId,
                Tags = []
            };
            _sectionRepository.Setup(r => r.GetSectionIncludeReferencesByIdAsync(request.SectionId, _cancellationToken))
                .ReturnsAsync(mockEntity);
            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT0.Should().BeTrue();
        }
    }
}
