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
        
        [HttpGet("GetWishlist")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlist()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            if (userId == null) {
                return Unauthorized();
            }
            int userID = (userId);
            var items= await _wishlistServices.GetWishlist(userID);
            if (items == null) return null;
            return Ok(items);



        }
        
        [HttpPost("AddToWishlist")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<string>> Addtowishlist(WishlistDto request)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            if (userId == null)
            {
                return Unauthorized();
            }
            
            var status = await _wishlistServices.AddToWishlist(request, userId);
            if (status is null) return BadRequest();
            return Ok(status);
        }
        
        [HttpDelete("RemoveWishlist")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<string>> RemoveFromWishlist(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            if (userId == null)
            {
                return Unauthorized();
            }
           
            var status = await _wishlistServices.RemoveWishlist(id, userId);
            if (status is null) return NotFound();
            return Ok(status);
        }



    }
}
