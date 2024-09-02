using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Logic.Results;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using GymInnowise.UserService.Shared.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Services
{
    public class PersonalGoalService(IPersonalGoalRepository _goalRepo) : IPersonalGoalService

    {
        public async Task CreateGoalAsync(CreatePersonalGoalRequest request)
        {
            var goalModel = new PersonalGoalModel()
            {
                Owner = request.Owner,
                Goal = request.Goal,
                SupervisorCoach = request.SupervisorCoach,
                StartDate = request.StartDate,
                DeadLine = request.DeadLine,
                Status = DateTime.UtcNow < request.StartDate ? GoalStatus.NotStarted : GoalStatus.InProgress
            };

            await _goalRepo.CreatePersonalGoalAsync(goalModel);
        }

        public async Task<OneOf<Success, GoalNotFound>> UpdatePersonalGoalAsync(UpdatePersonalGoalRequest request)
        {
            var goal = await _goalRepo.GetPersonalGoalAsync(request.Id);
            if (goal is null)
            {
                return new GoalNotFound();
            }

            goal.Goal = request.Goal;
            goal.SupervisorCoach = request.SupervisorCoach;
            goal.Status = request.Status;
            goal.StartDate = request.StartDate;
            goal.DeadLine = request.DeadLine;

            await _goalRepo.UpdatePersonalGoalAsync(goal);

            return new Success();
        }

        public async Task<OneOf<GetPersonalGoalResponse, GoalNotFound>> GetPersonalGoalAsync(Guid goalId)
        {
            var goalModel = await _goalRepo.GetPersonalGoalAsync(goalId);
            if (goalModel is null)
            {
                return new GoalNotFound();
            }

            return new GetPersonalGoalResponse()
            {
                Goal = goalModel.Goal,
                SupervisorCoach = goalModel.SupervisorCoach,
                Status = goalModel.Status,
                StartDate = goalModel.StartDate,
                DeadLine = goalModel.DeadLine
            };
        }

        public async Task<OneOf<Success, GoalNotFound>> RemovePersonalGoalAsync(Guid id)
        {
            if (await _goalRepo.GetPersonalGoalAsync(id) is null)
            {
                return new GoalNotFound();
            }

            await _goalRepo.RemovePersonalGoalAsync(id);

            return new Success();
        }
    }
}
