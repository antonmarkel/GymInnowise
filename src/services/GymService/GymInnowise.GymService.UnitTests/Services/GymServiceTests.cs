using AutoMapper;
using FakeItEasy;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;

namespace GymInnowise.GymService.UnitTests.Services
{
    public class GymServiceTests
    {
        private readonly IGymRepository _repo;
        private readonly Logic.Services.GymService _gymService;

        public GymServiceTests()
        {
            _repo = A.Fake<IGymRepository>();
            _gymService = new Logic.Services.GymService(_repo, A.Fake<IMapper>());
        }

        [Fact]
        public async Task UpdateGymAsync_NotFound_ReturnsNotFound()
        {
            //Arrange
            var request = new UpdateGymRequest();
            A.CallTo(() => _repo.GetGymByIdAsync(Guid.Empty)).Returns(Task.FromResult<GymEntity?>(null));

            //Act
            var result = await _gymService.UpdateGymAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _repo.UpdateGymAsync(A<GymEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateGymAsync_Success_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateGymRequest();
            A.CallTo(() => _repo.GetGymByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<GymEntity?>(
                    new GymEntity()
                        { Name = "name", Address = "address", ContactInfo = "ContactInfo" }));

            //Act
            var result = await _gymService.UpdateGymAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _repo.UpdateGymAsync(A<GymEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetGymDetailsByIdAsync_NotFound_ReturnsNotFound()
        {
            //Arrange
            A.CallTo(() => _repo.GetGymByIdAsync(Guid.Empty)).Returns(Task.FromResult<GymEntity?>(null));

            //Act
            var result = await _gymService.GetGymDetailsByIdAsync(Guid.Empty);

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetGymDetailsByIdAsync_Success_ReturnsSuccess()
        {
            //Arrange
            A.CallTo(() => _repo.GetGymByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<GymEntity?>(
                    new GymEntity()
                        { Name = "name", Address = "address", ContactInfo = "ContactInfo" }));
            //Act
            var result = await _gymService.GetGymDetailsByIdAsync(Guid.Empty);

            //Assert
            Assert.True(result.IsT0);
        }
    }
}
