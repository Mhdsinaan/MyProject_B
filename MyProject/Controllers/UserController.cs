using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using MyProject.CommenApi;
using MyProject.Interfaces;
using MyProject.Models.User;
using MyProject.Models.UserModel;
using MyProject.Services;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(new APiResponds<string>("400", "Invalid data", null));
            }
            var reg = await _userService.RegisterUser(request);
            if (reg == null)
            {
                return BadRequest(new APiResponds<string>("400", "Registration Failed", null));
            }
            var existing = await _userService.RegisterUser(request);
            if (existing == reg)
            {
                return StatusCode(500, new APiResponds<object>("407", "user already exist", false));

            }
            else
            {
                return Ok(new APiResponds<object>("200", "Registration Successful", reg));

            }




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


                var response = new LoginResponseDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    Token = user.Token,
                };

        return Ok(new APiResponds<LoginResponseDto>("200", "Login Successful", response));
    }
    catch (Exception ex)
    {
        return Unauthorized(new APiResponds<string>("401", "Login Failed", ex.Message));
    }
}


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _userService.FindById(id);
            if (result == null)
            {
                return NotFound(new APiResponds<string>("404", "User not found", null));
            }
            return Ok(new APiResponds<object>("200", "User found", result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("BlockUser/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var result = await _userService.BlockUser(id);
            if (result == null)
            {
                return NotFound(new APiResponds<string>("404", "User not found or cannot block", null));
            }

            return Ok(new APiResponds<string>("200", "User blocked successfully", result));
        }

        [Authorize(Roles = "Admin")]

        [HttpPut("UnblockUser/{id}")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            var result = await _userService.UnblockUser(id);
            if (result == null)
            {
                return NotFound(new APiResponds<string>("404", "User not found or cannot unblock", null));
            }

            return Ok(new APiResponds<string>("200", "User unblocked successfully", result));
        }
        [HttpGet("all-users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Users>>> AllUsers()
        {
            var usersData = await _userService.AllUsers();

            if (usersData == null)
            {
                return NotFound("No users found.");
            }

            return Ok(usersData);
        }


    }

}
