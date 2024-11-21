using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Handlers.Sections;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using Moq;

namespace GymInnowise.SectionService.Tests.SectionHandlersTests
{
    public class UpdateSectionHandlerTests
    {
        private readonly Mock<ISectionRepository> _sectionRepository;
        private readonly CancellationToken _cancellationToken;
        private readonly UpdateSectionHandler _handler;
        private readonly IFixture _fixture;

        public UpdateSectionHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _sectionRepository = _fixture.Freeze<Mock<ISectionRepository>>();
            _cancellationToken = _fixture.Freeze<CancellationToken>();
            _handler = _fixture.Create<UpdateSectionHandler>();
        }

        [Fact]
        public async Task UpdateSectionHandle_SectionNotFound_ReturnsNotFound()
        {
            //Arrange
            var request = _fixture.Create<UpdateSectionCommand>();
            _sectionRepository.Setup(r => r.ExistsByIdAsync(request.SectionId, _cancellationToken))
                .ReturnsAsync(false);
            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateSectionHandle_SectionWasFound_ReturnsSuccess()
        {
            //Arrange
            var request = _fixture.Create<UpdateSectionCommand>();
            _sectionRepository.Setup(r => r.ExistsByIdAsync(request.SectionId, _cancellationToken))
                .ReturnsAsync(true);
            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsT0.Should().BeTrue();
        }
    }
}
