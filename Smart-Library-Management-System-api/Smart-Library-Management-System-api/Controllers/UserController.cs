//using Microsoft.AspNetCore.Mvc;
//using Smart_Library_Management_System_api.SmartLibrary.Services;

//namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly UserService userServices;

//        public UserController(UserService userServices)
//        {
//            userServices = userServices;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetUsers()
//        {
//            return Ok(await userServices.GetAllUsers());
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUser(string id)
//        {
//            var user = await userServices.GetUserById(id);
//            if (user == null) return NotFound();
//            return Ok(user);
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // FIXED: Changed to use interface instead of concrete class
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}