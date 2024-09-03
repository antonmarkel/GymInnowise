using GymInnowise.UserService.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        [HttpPost("Get")]
        public async Task<IActionResult> GetAsync([FromBody] Guid id, IClientProfileRepository rep)
        {
            return Ok(await rep.GetClientProfileByIdAsync(id));
        }
    }
}
