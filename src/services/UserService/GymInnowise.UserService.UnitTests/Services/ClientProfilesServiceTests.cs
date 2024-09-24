using Castle.Core.Logging;
using FakeItEasy;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.Extensions.Logging;

namespace GymInnowise.UserService.UnitTests.Services
{
    public class ClientProfileServiceTests
    {
        private readonly IProfileRepository<ClientProfileEntity> _clientRepo;
        private readonly ClientProfileService _clientProfileService;

        public ClientProfileServiceTests()
        {
            _clientRepo = A.Fake<IProfileRepository<ClientProfileEntity>>();

            _clientProfileService = new ClientProfileService(_clientRepo, A.Fake<ILogger<ClientProfileService>>());
        }

        [Fact]
        public async Task CreateClientProfileAsync_ProfileAlreadyExists_ReturnsProfileAlreadyExists()
        {
            //Arrange
            var request = new CreateClientProfileRequest();
            A.CallTo(() => _clientRepo.DoesProfileExistAsync(request.AccountId)).Returns(true);

            //Act
            var result = await _clientProfileService.CreateClientProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _clientRepo.CreateProfileAsync(A<ClientProfileEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreateClientProfileAsync_NewProfile_ReturnsSuccess()
        {
            //Arrange
            var request = new CreateClientProfileRequest();
            A.CallTo(() => _clientRepo.DoesProfileExistAsync(request.AccountId)).Returns(false);

            //Act
            var result = await _clientProfileService.CreateClientProfileAsync(Guid.Empty, request);

            // Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _clientRepo.CreateProfileAsync(A<ClientProfileEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateClientProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateClientProfileRequest();
            A.CallTo(() => _clientRepo.GetProfileByIdAsync(Guid.Empty))!
                .Returns(Task.FromResult<ClientProfileEntity?>(null));

            //Act
            var result = await _clientProfileService.UpdateClientProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _clientRepo.UpdateProfileAsync(A<ClientProfileEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateClientProfileAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateClientProfileRequest();
            A.CallTo(() => _clientRepo.GetProfileByIdAsync(Guid.Empty))
                .Returns(new ClientProfileEntity() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _clientProfileService.UpdateClientProfileAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _clientRepo.UpdateProfileAsync(A<ClientProfileEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateClientProfileStatusAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateClientProfileStatusRequest();
            A.CallTo(() => _clientRepo.GetProfileByIdAsync(Guid.Empty))!
                .Returns(Task.FromResult<ClientProfileEntity?>(null));

            //Act
            var result = await _clientProfileService.UpdateClientProfileStatusAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _clientRepo.UpdateProfileAsync(A<ClientProfileEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateClientProfileStatusAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateClientProfileStatusRequest();
            A.CallTo(() => _clientRepo.GetProfileByIdAsync(Guid.Empty))
                .Returns(new ClientProfileEntity() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _clientProfileService.UpdateClientProfileStatusAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _clientRepo.UpdateProfileAsync(A<ClientProfileEntity>._))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetClientProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            A.CallTo(() => _clientRepo.GetProfileByIdAsync(Guid.Empty))
                .Returns(Task.FromResult<ClientProfileEntity?>(null));

            //Act
            var result = await _clientProfileService.GetClientProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetClientProfileAsync_ProfileExists_ReturnsGetClientProfileResponse()
        {
            //Arrange
            A.CallTo(() => _clientRepo.GetProfileByIdAsync(new Guid()))
                .Returns(new ClientProfileEntity() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _clientProfileService.GetClientProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT0);
        }
    }
}