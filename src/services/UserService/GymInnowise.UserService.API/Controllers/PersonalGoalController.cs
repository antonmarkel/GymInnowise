using GymInnowise.UserService.API.Authorization;
using GymInnowise.UserService.Logic.Helpers;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Authorization;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/personal-goals")]
    public class PersonalGoalController : ControllerBase
    {
        private readonly IPersonalGoalService _personalGoalService;

        public PersonalGoalController(IPersonalGoalService personalGoalService)
        {
            _personalGoalService = personalGoalService;
        }

        [Authorize]
        [OwnerOrAdminAuthorize(nameof(ownerId))]
        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetPersonalGoalsAsync(Guid ownerId)
        {
            var goals = await _personalGoalService.GetAllPersonalGoalsAsync(ownerId);

            return Ok(goals);
        }

        [Authorize(Roles = Roles.Coach)]
        [HttpGet("{ownerId}/supervised-goals")]
        public async Task<IActionResult> GetCoachSupervisedGoalsAsync(Guid ownerId)
        {
            var coachId = ClaimsHelper.GetAccountId(User.Claims);
            var goals = await _personalGoalService.GetCoachSupervisedGoalsAsync(ownerId, coachId);

            return Ok(goals);
        }

        [Authorize]
        [HttpPut("{goalId}")]
        public async Task<IActionResult> UpdatePersonalGoalAsync(Guid goalId,
            [FromBody] UpdatePersonalGoalRequest request)
        {
            var goalResult = await _personalGoalService.GetPersonalGoalAsync(goalId);
            if (goalResult.IsT1)
            {
                return NotFound();
            }

            var goal = goalResult.AsT0;
            var accountId = ClaimsHelper.GetAccountId(User.Claims);
            if (goal.Owner != accountId && !User.IsInRole(Roles.Admin) &&
                !(goal.SupervisorCoach == accountId && User.IsInRole(Roles.Coach)))
            {
                return Forbid();
            }

            var updateResult = await _personalGoalService.UpdatePersonalGoalAsync(goalId, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound());
        }

        [Authorize]
        [OwnerOrAdminAuthorize(nameof(ownerId))]
        [HttpPost("{ownerId}")]
        public async Task<IActionResult> CreatePersonalGoalAsync(Guid ownerId,
            [FromBody] CreatePersonalGoalRequest request)
        {
            await _personalGoalService.CreatePersonalGoalAsync(ownerId, request);

            return Created();
        }
    }
}