using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.User;

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
        public async Task<IActionResult> Registration([FromBody]RegisterDto request)
        {
            var reg= await _userService.RegisterUser(request);
            if(reg == null)
            {
                return BadRequest();
            }

           return  Ok(reg);
           
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var logg = await _userService.LoginUser(request);
            if (logg == null)
            {
                return NotFound("no matched data");

            }
            return Ok(logg);
            
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
