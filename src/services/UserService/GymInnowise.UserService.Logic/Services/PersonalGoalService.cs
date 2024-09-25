using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Persistence.Models;
using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets;
using GymInnowise.UserService.Shared.Enums;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.UserService.Logic.Services
{
    public class PersonalGoalService(IPersonalGoalRepository _goalRepo, ILogger<PersonalGoalService> _logger)
        : IPersonalGoalService
    {
        public async Task CreatePersonalGoalAsync(Guid ownerId, CreatePersonalGoalRequest request)
        {
            var goalModel = new PersonalGoalEntity()
            {
                Owner = ownerId,
                Goal = request.Goal,
                SupervisorCoach = request.SupervisorCoach,
                StartDate = request.StartDate,
                DeadLine = request.DeadLine,
                Status = DateTime.UtcNow < request.StartDate ? GoalStatus.NotStarted : GoalStatus.InProgress
            };
            _logger.LogInformation("Creating a new goal {@goalModel}, for {@ownerId}", goalModel, ownerId);
            await _goalRepo.CreatePersonalGoalAsync(goalModel);
        }

        public async Task<OneOf<Success, NotFound>> UpdatePersonalGoalAsync(Guid goalId,
            UpdatePersonalGoalRequest request)
        {
            var goal = await _goalRepo.GetPersonalGoalAsync(goalId);
            if (goal is null)
            {
                _logger.LogWarning(
                    "Goal wasn't updated. Reason: goal with this id {@goalId} was not found!",
                    goalId);

                return new NotFound();
            }

            goal.Goal = request.Goal;
            goal.SupervisorCoach = request.SupervisorCoach;
            goal.Status = request.Status;
            goal.StartDate = request.StartDate;
            goal.DeadLine = request.DeadLine;

            await _goalRepo.UpdatePersonalGoalAsync(goal);
            _logger.LogInformation(
                "Goal was updated successfully. Info: {@goalId}",
                goalId);

            return new Success();
        }

        public async Task<List<GetPersonalGoalResponse>> GetAllPersonalGoalsAsync(Guid ownerId)
        {
            var goals = await _goalRepo.GetAllPersonalGoalsAsync(ownerId);

            return goals.Select(g => new GetPersonalGoalResponse()
            {
                Owner = ownerId,
                Goal = g.Goal,
                SupervisorCoach = g.SupervisorCoach,
                Status = g.Status,
                StartDate = g.StartDate,
                DeadLine = g.DeadLine
            }).ToList();
        }

        public async Task<List<GetPersonalGoalResponse>> GetCoachSupervisedGoalsAsync(Guid ownerId, Guid coachId)
        {
            var goals = await _goalRepo.GetCoachSupervisedGoalsAsync(ownerId, coachId);

            return goals.Select(g => new GetPersonalGoalResponse()
            {
                Owner = ownerId,
                Goal = g.Goal,
                SupervisorCoach = g.SupervisorCoach,
                Status = g.Status,
                StartDate = g.StartDate,
                DeadLine = g.DeadLine
            }).ToList();
        }

        public async Task<OneOf<GetPersonalGoalResponse, NotFound>> GetPersonalGoalAsync(Guid goalId)
        {
            var goalEntity = await _goalRepo.GetPersonalGoalAsync(goalId);
            if (goalEntity is null)
            {
                _logger.LogWarning(
                    "Goal with this id: {@goalId} was not found!",
                    goalId);

                return new NotFound();
            }

            return new GetPersonalGoalResponse()
            {
                Owner = goalEntity.Owner,
                Goal = goalEntity.Goal,
                SupervisorCoach = goalEntity.SupervisorCoach,
                Status = goalEntity.Status,
                StartDate = goalEntity.StartDate,
                DeadLine = goalEntity.DeadLine
            };
        }
    }
}