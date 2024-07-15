using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.Authorization.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAccounts(IAccountsRepository repo)
        {
            return Ok(new { Client = await repo.GetAllAccountsAsync() });
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(IRolesRepository repo)
        {
            return Ok(new { Client = await repo.GetAllRolesAsync() });
        }
    }
}
