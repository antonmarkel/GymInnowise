using Castle.Core.Logging;
using FakeItEasy;
using GymInnowise.UserService.Logic.Services;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.Extensions.Logging;

namespace GymInnowise.UserService.UnitTests.Services
{
    public class PersonalGoalServiceTests
    {
        private readonly IPersonalGoalRepository _goalRepo;
        private readonly PersonalGoalService _personalGoalService;

        public PersonalGoalServiceTests()
        {
            _goalRepo = A.Fake<IPersonalGoalRepository>();
            _personalGoalService = new PersonalGoalService(_goalRepo, A.Fake<ILogger<PersonalGoalService>>());
        }

        [Fact]
        public async Task UpdatePersonalGoalAsync_GoalNotFound_ReturnsGoalNotFound()
        {
            //Arrange
            var request = new UpdatePersonalGoalRequest();
            A.CallTo(() =>
                _goalRepo.GetPersonalGoalAsync(new Guid())).Returns(Task.FromResult<PersonalGoalEntity?>(null));

            //Act
            var result = await _personalGoalService.UpdatePersonalGoalAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT1);
            A.CallTo(() => _goalRepo.UpdatePersonalGoalAsync(A<PersonalGoalEntity>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdatePersonalGoalAsync_GoalFound_ReturnsSuccess()
        {
            //Arrange
            var request = new UpdatePersonalGoalRequest();
            A.CallTo(() =>
                _goalRepo.GetPersonalGoalAsync(new Guid())).Returns(new PersonalGoalEntity() { Goal = "Goal" });

            //Act
            var result = await _personalGoalService.UpdatePersonalGoalAsync(Guid.Empty, request);

            //Assert
            Assert.True(result.IsT0);
            A.CallTo(() => _goalRepo.UpdatePersonalGoalAsync(A<PersonalGoalEntity>._)).MustHaveHappenedOnceExactly();
        }
    }
}
