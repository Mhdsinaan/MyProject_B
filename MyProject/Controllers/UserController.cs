using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Context;
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
                return BadRequest();
            }

            return Ok(reg);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await _userService.LoginUser(request);

            if (user == null)
            {
                return NotFound("No matching user found.");
            }

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _userService.FindById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        

    }

}
