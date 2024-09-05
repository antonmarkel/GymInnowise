using FakeItEasy;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

namespace GymInnowise.UserService.UnitTests.Services
{
    public class CoachProfilesServiceTests
    {
        private readonly ICoachProfileRepository _coachRepo;
        private readonly CoachProfileService _coachProfileService;

        public CoachProfilesServiceTests()
        {
            _coachRepo = A.Fake<ICoachProfileRepository>();
            _coachProfileService = new CoachProfileService(_coachRepo);
        }

        [Fact]
        public async Task CreateCoachProfileAsync_ProfileAlreadyExists_ReturnsProfileAlreadyExists()
        {
            //Arrange
            var request = new CreateCoachProfileRequest();
            A.CallTo(() => _coachRepo.DoesProfileExistAsync(request.AccountId)).Returns(true);

            //Act
            var result = await _coachProfileService.CreateCoachProfileAsync(request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _coachRepo.CreateCoachProfileAsync(A<CoachProfileModel>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreateCoachProfileAsync_NewProfile_ReturnsSuccess()
        {
            //Arrange
            var request = new CreateCoachProfileRequest();
            A.CallTo(() => _coachRepo.DoesProfileExistAsync(request.AccountId)).Returns(false);

            //Act
            var result = await _coachProfileService.CreateCoachProfileAsync(request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _coachRepo.CreateCoachProfileAsync(A<CoachProfileModel>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateCoachProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateCoachProfileRequest();
            A.CallTo(() =>
                _coachRepo.GetCoachProfileByIdAsync(new Guid())).Returns(Task.FromResult<CoachProfileModel?>(null));

            //Act
            var result = await _coachProfileService.UpdateCoachProfileAsync(request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _coachRepo.UpdateCoachProfileAsync(A<CoachProfileModel>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateCoachProfileAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateCoachProfileRequest();
            A.CallTo(() =>
                _coachRepo.GetCoachProfileByIdAsync(new Guid())).Returns(new CoachProfileModel()
            {
                FirstName = "Bob",
                LastName = "Coach"
            });

            //Act
            var result = await _coachProfileService.UpdateCoachProfileAsync(request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _coachRepo.UpdateCoachProfileAsync(A<CoachProfileModel>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateCoachProfileStatusAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateCoachProfileStatusRequest();
            A.CallTo(() =>
                _coachRepo.GetCoachProfileByIdAsync(new Guid())).Returns(Task.FromResult<CoachProfileModel?>(null));

            //Act
            var result = await _coachProfileService.UpdateCoachProfileStatusAsync(request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _coachRepo.UpdateCoachProfileAsync(A<CoachProfileModel>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateCoachProfileStatusAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateCoachProfileStatusRequest();
            A.CallTo(() =>
                _coachRepo.GetCoachProfileByIdAsync(new Guid())).Returns(new CoachProfileModel()
            {
                FirstName = "Bob",
                LastName = "Coach"
            });

            //Act
            var result = await _coachProfileService.UpdateCoachProfileStatusAsync(request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _coachRepo.UpdateCoachProfileAsync(A<CoachProfileModel>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetCoachProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            A.CallTo(() => _coachRepo.GetCoachProfileByIdAsync(new Guid()))
                .Returns(Task.FromResult<CoachProfileModel?>(null));

            //Act
            var result = await _coachProfileService.GetCoachProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetClientProfileAsync_ProfileExists_ReturnsGetClientProfileResponse()
        {
            //Arrange
            A.CallTo(() => _coachRepo.GetCoachProfileByIdAsync(new Guid()))
                .Returns(new CoachProfileModel() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _coachProfileService.GetCoachProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT0);
        }
    }
}
