using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using System.Security.Claims;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartproducts _cartService;

        public CartController(ICartproducts cartService)
        {
            _cartService = cartService;
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDtos cart)
        {
            var result = await _cartService.AddToCart(cart);
            if (result == null)
                return BadRequest("User or product not found.");
            return Ok(result);
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = new Users { Id = int.Parse(userId) };
            var items = await _cartService.GetCartItems(user);
            return Ok(items);
        }

        // DELETE: api/cart/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = new Users { Id = int.Parse(userId) };
            var removed = await _cartService.RemoveFromCart(id, user);

            if (removed == null)
                return NotFound("Cart item not found.");

            return Ok(removed);
        }

        // PUT: api/cart/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] CartDtos cart)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = new Users { Id = int.Parse(userId) };
            var updated = await _cartService.UpdateCart(id, cart, user);

            if (updated == null)
                return NotFound("Cart item not found.");

            return Ok(updated);
        }
    }
}
