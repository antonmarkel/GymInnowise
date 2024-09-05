﻿using GymInnowise.UserService.API.Authorization;
using GymInnowise.UserService.Logic.Interfaces;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/profile/[controller]")]
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
            var goals = await _personalGoalService.GetAllPersonalGoalsAsync(ownerId);

            return Ok(goals);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePersonalGoalAsync([FromBody] UpdatePersonalGoalRequest request)
        {
            var owner = await _personalGoalService.GetOwnerAsync(request.Id);
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, owner, PolicyNames.OwnerOrAdmin);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var updateResult = await _personalGoalService.UpdatePersonalGoalAsync(request);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePersonalGoalAsync([FromBody] CreatePersonalGoalRequest request)
        {
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, request.Owner, PolicyNames.OwnerOrAdmin);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _personalGoalService.CreatePersonalGoalAsync(request);
            var userAccountId = User.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value;

            Console.WriteLine($"Account id is {userAccountId} and should be {request.Owner}");

            return Created();
        }

        [HttpDelete("goalId")]
        public async Task<IActionResult> RemovePersonalGoalAsync(Guid goalId)
        {
            var owner = await _personalGoalService.GetOwnerAsync(goalId);
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, owner, PolicyNames.OwnerOrAdmin);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _personalGoalService.RemovePersonalGoalAsync(goalId);

            return NoContent();
        }
    }
}