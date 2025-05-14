using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.CommenApi;
using MyProject.Interfaces;
using MyProject.Models.User;
using MyProject.Models.UserModel;

namespace MyProject.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterDto request)
        {
            var reg = await _userService.RegisterUser(request);
            if (reg == null)
            {
                return BadRequest(new APiResponds<string>("400", "Registration Failed", null));
            }

            return Ok(new APiResponds<object>("200", "Registration Successful", reg));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            try
            {
                var user = await _userService.LoginUser(request);
                if (user == null)
                {
                    return Unauthorized(new APiResponds<string>("401", "Invalid credentials", null));
                }

                return Ok(new APiResponds<object>("200", "Login Successful", user));
            }
            catch (Exception ex)
            {
                return Unauthorized(new APiResponds<string>("401", "Login Failed",  ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _userService.FindById(id);
            if (result == null)
            {
                return NotFound(new APiResponds<string>("404", "User not found",  null));
            }
            return Ok(new APiResponds<object>("200", "User found", result));
        }
    }
}

