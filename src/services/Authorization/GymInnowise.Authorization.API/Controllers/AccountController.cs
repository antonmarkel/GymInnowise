using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.Authorization.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            var registerResult = await _accountService.RegisterAsync(registerRequest);

            return registerResult.Match<IActionResult>(
                _ => Created(),
                _ => BadRequest("Account with this email or mobile phone already exists! Try to log in!")
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateRequest updateRequest)
        {
            var accountId = ClaimsHelper.GetAccountId(User.Claims);
            if (id != accountId && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            var updateResult = await _accountService.UpdateAsync(id, updateRequest);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}/roles")]
        public async Task<IActionResult> UpdateRolesAsync(Guid id, [FromBody] UpdateRolesRequest updateRolesRequest)
        {
            var updateResult = await _accountService.UpdateRolesAsync(id, updateRolesRequest);

            return updateResult.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }
    }
}