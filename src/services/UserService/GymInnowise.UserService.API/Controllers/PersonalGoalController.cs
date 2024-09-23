using GymInnowise.UserService.API.Authorization;
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
        private readonly IAuthorizationService _authorizationService;

        public PersonalGoalController(IPersonalGoalService personalGoalService,
            IAuthorizationService authorizationService)
        {
            _personalGoalService = personalGoalService;
            _authorizationService = authorizationService;
        }

        [Authorize]
        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetPersonalGoalsAsync(Guid ownerId)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, ownerId, PolicyNames.OwnerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var goals = await _personalGoalService.GetAllPersonalGoalsAsync(ownerId);

            return Ok(goals);
        }

        [Authorize(Roles = Roles.CoachOrAdmin)]
        [HttpGet("{ownerId}/supervised-goals")]
        public async Task<IActionResult> GetCoachSupervisedGoalsAsync(Guid ownerId)
        {
            var accountIdClaim = User.Claims.FirstOrDefault(c => c.Type == "accountId")!.Value;

            if (!Guid.TryParse(accountIdClaim, out Guid coachId))
            {
                return BadRequest("Invalid AccountId format");
            }

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

            var authOwnerResult =
                await _authorizationService.AuthorizeAsync(User, goal.Owner.ToString(),
                    PolicyNames.OwnerPolicy);
            var authCoachResult = await _authorizationService.AuthorizeAsync(User,
                goal.SupervisorCoach?.ToString() ?? string.Empty, PolicyNames.SupervisorPolicy);
            if (!authOwnerResult.Succeeded && !authCoachResult.Succeeded)
            {
                return Forbid();
            }

            var updateResult = await _personalGoalService.UpdatePersonalGoalAsync(goalId, request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePersonalGoalAsync([FromBody] CreatePersonalGoalRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, request.Owner,
                    PolicyNames.OwnerPolicy);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _personalGoalService.CreatePersonalGoalAsync(request);
            var userAccountId = User.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value;

            Console.WriteLine($"Account id is {userAccountId} and should be {request.Owner}");

            return Created();
        }
    }
}