using Microsoft.AspNetCore.Mvc;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.Controllers
{
    [ApiController]
    [Route("mvc/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _userService.GetAllUsersAsync();
            return Ok(list);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
