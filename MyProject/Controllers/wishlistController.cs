using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.CommenApi;
using MyProject.Interfaces;
using MyProject.Models.WishlistModel;
using MyProject.Services;

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
        public async Task<ActionResult<APiResponds<IEnumerable<WishListResDTO>>>> GetWishlist()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            if (userId == null)
            {
                return Unauthorized(new APiResponds<string>("401", "Unauthorized: User not authenticated", null));

            }
            int userID = (userId);
            var items = await _wishlistServices.GetWishlist(userID);
            if (items == null)
            {
                return NotFound(new APiResponds<string>("404", "No wishlist items found.", null));

            }

            return Ok(new APiResponds<IEnumerable<WishListResDTO>>("200", "Wishlist retrieved successfully.", items));




        }

        [HttpPost("AddToWishlist")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<string>> Addtowishlist(WishlistDto request)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            if (userId == null)
            {
                return Unauthorized(new APiResponds<string>("401", "Unauthorized: User not authenticated", null));
            }

            var status = await _wishlistServices.AddToWishlist(request, userId);

            if (status is null)
            {
                return BadRequest(new APiResponds<string>("400", "Failed to add item to wishlist", null));
            }

            return Ok(new APiResponds<Wishlist>("200", "Item added to wishlist successfully", null));



        }

        [HttpDelete("RemoveWishlist/{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<APiResponds<string>>> RemoveFromWishlist(int id)
        {
            if (!HttpContext.Items.ContainsKey("UserId"))
            {
                return Unauthorized(new APiResponds<string>("401", "Unauthorized: User not authenticated", null));
            }

            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            var result = await _wishlistServices.RemoveWishlist(id, userId);

            if (result == null)
            {
                return NotFound(new APiResponds<string>("404", "Wishlist item not found", null));
            }

            return Ok(new APiResponds<WishListResDTO>("200", "Item removed from wishlist successfully", null));
        }

    }
}
