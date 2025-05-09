using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;
using MyProject.Models.WishlistModel;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class wishlistController : ControllerBase
    {
        private readonly IWishlistServices _wishlistServices;
        public wishlistController(IWishlistServices wishlistServices)
        {
            _wishlistServices = wishlistServices;
        }
        [Authorize]
        [HttpGet("GetWishlist")]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlist()
        {
            var user= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null) {
                return Unauthorized();
            }
            int userID = int.Parse(user);
            var items= await _wishlistServices.GetWishlist(userID);
            if (items == null) return null;
            return Ok(items);



        }
        [Authorize]
        [HttpPost("AddToWishlist")]
        public async Task<ActionResult<string>> Addtowishlist(WishlistDto request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            int use = int.Parse(userId);
            var status = await _wishlistServices.AddToWishlist(request, use);
            if (status is null) return BadRequest();
            return Ok(status);
        }
        [Authorize]
        [HttpDelete("RemoveWishlist")]
        public async Task<ActionResult<string>> RemoveFromWishlist(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            int userIdguid = int.Parse(userId);
            var status = await _wishlistServices.RemoveWishlist(id, userIdguid);
            if (status is null) return NotFound();
            return Ok(status);
        }



    }
}
