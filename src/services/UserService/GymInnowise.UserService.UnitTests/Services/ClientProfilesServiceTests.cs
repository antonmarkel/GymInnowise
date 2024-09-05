using FakeItEasy;
using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using OneOf.Types;

namespace GymInnowise.UserService.UnitTests.Services
{
    public class ClientProfileServiceTests
    {
        private readonly IClientProfileRepository _clientRepo;
        private readonly ClientProfileService _clientProfileService;

        public ClientProfileServiceTests()
        {
            _clientRepo = A.Fake<IClientProfileRepository>();
            _clientProfileService = new ClientProfileService(_clientRepo);
        }

        [Fact]
        public async Task CreateClientProfileAsync_ProfileAlreadyExists_ReturnsProfileAlreadyExists()
        {
            //Arrange
            var request = new CreateClientProfileRequest();
            A.CallTo(() => _clientRepo.DoesProfileExistAsync(request.AccountId)).Returns(true);

            //Act
            var result = await _clientProfileService.CreateClientProfileAsync(request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _clientRepo.CreateClientProfileAsync(A<ClientProfileModel>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreateClientProfileAsync_NewProfile_ReturnsSuccess()
        {
            //Arrange
            var request = new CreateClientProfileRequest();
            A.CallTo(() => _clientRepo.DoesProfileExistAsync(request.AccountId)).Returns(false);

            //Act
            var result = await _clientProfileService.CreateClientProfileAsync(request);

            // Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _clientRepo.CreateClientProfileAsync(A<ClientProfileModel>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateClientProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateClientProfileRequest();
            A.CallTo(() => _clientRepo.GetClientProfileByIdAsync(request.AccountId))!
                .Returns(Task.FromResult<ClientProfileModel?>(null));

            //Act
            var result = await _clientProfileService.UpdateClientProfileAsync(request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _clientRepo.UpdateClientProfileAsync(A<ClientProfileModel>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateClientProfileAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateClientProfileRequest();
            A.CallTo(() => _clientRepo.GetClientProfileByIdAsync(request.AccountId))
                .Returns(new ClientProfileModel() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _clientProfileService.UpdateClientProfileAsync(request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _clientRepo.UpdateClientProfileAsync(A<ClientProfileModel>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateClientProfileStatusAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateClientProfileStatusRequest();
            A.CallTo(() => _clientRepo.GetClientProfileByIdAsync(request.AccountId))!
                .Returns(Task.FromResult<ClientProfileModel?>(null));

            //Act
            var result = await _clientProfileService.UpdateClientProfileStatusAsync(request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _clientRepo.UpdateClientProfileAsync(A<ClientProfileModel>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateClientProfileStatusAsync_ProfileExists_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdateClientProfileStatusRequest();
            A.CallTo(() => _clientRepo.GetClientProfileByIdAsync(request.AccountId))
                .Returns(new ClientProfileModel() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _clientProfileService.UpdateClientProfileStatusAsync(request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _clientRepo.UpdateClientProfileAsync(A<ClientProfileModel>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetClientProfileAsync_ProfileNotFound_ReturnsProfileNotFound()
        {
            //Arrange
            var request = new UpdateClientProfileStatusRequest();
            A.CallTo(() => _clientRepo.GetClientProfileByIdAsync(request.AccountId))
                .Returns(Task.FromResult<ClientProfileModel?>(null));

            //Act
            var result = await _clientProfileService.GetClientProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetClientProfileAsync_ProfileExists_ReturnsGetClientProfileResponse()
        {
            //Arrange
            var request = new UpdateClientProfileStatusRequest();
            A.CallTo(() => _clientRepo.GetClientProfileByIdAsync(request.AccountId))
                .Returns(new ClientProfileModel() { FirstName = "Bob", LastName = "Flash" });

            //Act
            var result = await _clientProfileService.GetClientProfileAsync(new Guid());

            //Assert
            Assert.True(result.IsT0);
        }
    }
}