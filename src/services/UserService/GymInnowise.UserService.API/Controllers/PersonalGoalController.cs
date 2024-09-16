﻿using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
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

        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetPersonalGoalsAsync(Guid ownerId)
        {
            var goals = await _personalGoalService.GetAllPersonalGoalsAsync(ownerId);

            return Ok(goals);
        }

        [HttpPut("{goalId}")]
        public async Task<IActionResult> UpdatePersonalGoalAsync(Guid goalId,
            [FromBody] UpdatePersonalGoalRequest request)
        {
            var updateResult = await _personalGoalService.UpdatePersonalGoalAsync(goalId, request);

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
    }
}