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
        public async Task CreatePersonalGoalAsync(CreatePersonalGoalRequest request)
        {
            var goalModel = new PersonalGoalEntity()
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

        public async Task<OneOf<Success, GoalNotFound>> UpdatePersonalGoalAsync(Guid goalId,
            UpdatePersonalGoalRequest request)
        {
            var goal = await _goalRepo.GetPersonalGoalAsync(goalId);
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

        public async Task<List<GetPersonalGoalResponse>> GetAllPersonalGoalsAsync(Guid ownerId)
        {
            var goals = await _goalRepo.GetAllPersonalGoalsAsync(ownerId);

            return goals.Select(g => new GetPersonalGoalResponse()
            {
                Goal = g.Goal,
                SupervisorCoach = g.SupervisorCoach,
                Status = g.Status,
                StartDate = g.StartDate,
                DeadLine = g.DeadLine
            }).ToList();
        }
    }
}
