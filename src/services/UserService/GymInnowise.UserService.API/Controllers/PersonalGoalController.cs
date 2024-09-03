using System.Runtime.InteropServices;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/profile/{controller}")]
    public class PersonalGoalController : ControllerBase
    {
        private readonly IPersonalGoalService _personalGoalService;

        public PersonalGoalController(IPersonalGoalService personalGoalService)
        {
            _personalGoalService = personalGoalService;
        }

        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetPersonalGoalsAsync(Guid ownerId)
        {
            var goals = await _personalGoalService.GetAllPersonalGoalsAsync(ownerId);

            return Ok(goals);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersonalGoalAsync([FromBody] UpdatePersonalGoalRequest request)
        {
            var updateResult = await _personalGoalService.UpdatePersonalGoalAsync(request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalGoalAsync([FromBody] CreatePersonalGoalRequest request)
        {
            await _personalGoalService.CreatePersonalGoalAsync(request);

            return Created();
        }

        [HttpDelete("goalId")]
        public async Task<IActionResult> RemovePersonalGoalAsync(Guid goalId)
        {
            await _personalGoalService.RemovePersonalGoalAsync(goalId);

            return NoContent();
        }
    }
}
