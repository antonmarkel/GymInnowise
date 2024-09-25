using AutoMapper;
using FakeItEasy;
using GymInnowise.GymService.Logic.Services;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using Microsoft.Extensions.Logging;

namespace GymInnowise.GymService.UnitTests.Services
{
    public class GymEventServiceTests
    {
        private readonly IGymEventRepository _repo;
        private readonly GymEventService _gymEventService;

        public GymEventServiceTests()
        {
            _repo = A.Fake<IGymEventRepository>();
            _gymEventService = new GymEventService(_repo, A.Fake<IMapper>(), A.Fake<ILogger<GymEventService>>());
        }

        [Fact]
        public async Task UpdateGymEventAsync_NotFound_ReturnsNotFound()
        {
            //Arrange
            var request = new UpdateGymEventDtoRequest();
            A.CallTo(() => _repo.GetGymEventByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<GymEventEntity?>(null));

            //Act
            var result = await _gymEventService.UpdateGymEventAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _repo.UpdateEventAsync(A<GymEventEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateGymEventAsync_Success_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateGymEventDtoRequest();
            A.CallTo(() => _repo.GetGymEventByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<GymEventEntity?>(new GymEventEntity()));

            //Act
            var result = await _gymEventService.UpdateGymEventAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _repo.UpdateEventAsync(A<GymEventEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetGymIdAsync_NotFound_ReturnsNotFound()
        {
            //Arrange
            A.CallTo(() => _repo.GetGymEventByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<GymEventEntity?>(null));

            //Act
            var result = await _gymEventService.GetGymIdAsync(Guid.Empty);

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetGymIdAsync_Success_ReturnsSuccess()
        {
            //Arrange
            A.CallTo(() => _repo.GetGymEventByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<GymEventEntity?>(new GymEventEntity()));

            //Act
            var result = await _gymEventService.GetGymIdAsync(Guid.Empty);

            //Assert
            Assert.True(result.IsT0);
        }
    }
}