using Microsoft.AspNetCore.Mvc;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;
using SmartLibrary.DTOs.StudentDTOs;
using SmartLibrary.DTOs.FacultyDTOs;
using SmartLibrary.DTOs.UserDTOs;

namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound($"User {id} not found.");
            return Ok(user);
        }

        [HttpPost("student")]
        public async Task<IActionResult> RegisterStudent([FromBody] CreateStudentDTO dto)
        {
            try
            {
                var user = await _userService.RegisterStudent(dto);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("faculty")]
        public async Task<IActionResult> RegisterFaculty([FromBody] CreateFacultyDTO dto)
        {
            try
            {
                var user = await _userService.RegisterFaculty(dto);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                var updated = await _userService.UpdateUser(userId, dto);
                if (updated == null)
                    return NotFound($"User {userId} not found.");
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (!result)
                return NotFound($"User {userId} not found.");
            return Ok("User deleted successfully");
        }
    }
}