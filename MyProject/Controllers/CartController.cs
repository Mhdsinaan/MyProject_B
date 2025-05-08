using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var items = await _cartService.GetCartItems(userId);
            return Ok(items);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                var removedItem = await _cartService.RemoveFromCart(id);
                return Ok(removedItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("increment/{id}")]
        public async Task<IActionResult> IncrementQuantity(int id, [FromBody] CartDtos cart, [FromQuery] int userId)
        {
            try
            {
                var updatedItem = await _cartService.Increment(id, cart, userId);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("decrement/{id}")]
        public async Task<IActionResult> DecrementQuantity(int id, [FromQuery] int userId)
        {
            try
            {
                var updatedItem = await _cartService.Decrement(id, userId);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
