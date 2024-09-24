using FakeItEasy;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.Extensions.Logging;
using System;

namespace GymInnowise.UserService.UnitTests.Services
{
    public class CoachProfilesServiceTests
    {
        private readonly IProfileRepository<CoachProfileEntity> _coachRepo;
        private readonly CoachProfileService _coachProfileService;

        public CoachProfilesServiceTests()
        {
            _coachRepo = A.Fake<IProfileRepository<CoachProfileEntity>>();
            _coachProfileService = new CoachProfileService(_coachRepo, A.Fake<ILogger<CoachProfileService>>());
        }

        [Fact]
        public async Task CreateCoachProfileAsync_ProfileAlreadyExists_ReturnsProfileAlreadyExists()
        {
            //Arrange
            var request = new CreateCoachProfileRequest();
            A.CallTo(() => _coachRepo.DoesProfileExistAsync(Guid.Empty)).Returns(true);

            //Act
            var result = await _coachProfileService.CreateCoachProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _coachRepo.CreateProfileAsync(A<CoachProfileEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreateCoachProfileAsync_NewProfile_ReturnsSuccess()
        {
            //Arrange
            var request = new CreateCoachProfileRequest();
            A.CallTo(() => _coachRepo.DoesProfileExistAsync(Guid.Empty)).Returns(false);

            //Act
            var result = await _coachProfileService.CreateCoachProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _coachRepo.CreateProfileAsync(A<CoachProfileEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateCoachProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateCoachProfileRequest();
            A.CallTo(() =>
                _coachRepo.GetProfileByIdAsync(new Guid())).Returns(Task.FromResult<CoachProfileEntity?>(null));

            //Act
            var result = await _coachProfileService.UpdateCoachProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _coachRepo.UpdateProfileAsync(A<CoachProfileEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateCoachProfileAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateCoachProfileRequest();
            A.CallTo(() =>
                _coachRepo.GetProfileByIdAsync(new Guid())).Returns(new CoachProfileEntity()
            {
                FirstName = "Bob",
                LastName = "Coach"
            });

            //Act
            var result = await _coachProfileService.UpdateCoachProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _coachRepo.UpdateProfileAsync(A<CoachProfileEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateCoachProfileStatusAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateCoachProfileStatusRequest();
            A.CallTo(() =>
                _coachRepo.GetProfileByIdAsync(new Guid())).Returns(Task.FromResult<CoachProfileEntity?>(null));

            //Act
            var result = await _coachProfileService.UpdateCoachProfileStatusAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _coachRepo.UpdateProfileAsync(A<CoachProfileEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateCoachProfileStatusAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateCoachProfileStatusRequest();
            A.CallTo(() =>
                _coachRepo.GetProfileByIdAsync(new Guid())).Returns(new CoachProfileEntity()
            {
                FirstName = "Bob",
                LastName = "Coach"
            });

            //Act
            var result = await _coachProfileService.UpdateCoachProfileStatusAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _coachRepo.UpdateProfileAsync(A<CoachProfileEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetCoachProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            A.CallTo(() => _coachRepo.GetProfileByIdAsync(new Guid()))
                .Returns(Task.FromResult<CoachProfileEntity?>(null));

            //Act
            var result = await _coachProfileService.GetCoachProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetClientProfileAsync_ProfileExists_ReturnsGetClientProfileResponse()
        {
            //Arrange
            A.CallTo(() => _coachRepo.GetProfileByIdAsync(new Guid()))
                .Returns(new CoachProfileEntity() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _coachProfileService.GetCoachProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT0);
        }
    }
}