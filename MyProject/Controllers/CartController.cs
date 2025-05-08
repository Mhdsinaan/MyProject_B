using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models.CartModel;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;

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

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDtos cart)
        {
            var result = await _cartService.AddToCart(cart);
            if (result == null) return BadRequest("User not authorized or product not found");
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetCartItems(int userId)
        {
            var cartItems = await _cartService.GetCartItems(userId);
            return Ok(cartItems);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var removedItem = await _cartService.RemoveFromCart(id);
            if (removedItem == null) return NotFound("Item not found");
            return Ok(removedItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] CartDtos cart, [FromQuery] int userId)
        {
            var updated = await _cartService.UpdateCart(id, cart, userId);
            if (updated == null) return NotFound("Item not found or unauthorized");
            return Ok(updated);
        }
    }
}
