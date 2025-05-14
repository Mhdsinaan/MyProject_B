using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.CommenApi;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using MyProject.Models.ProductModel;
using MyProject.Services;
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


        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDtos cart)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new APiResponds<string>("401", "User not authorized", null));

            var result = await _cartService.AddToCart(cart, int.Parse(userId));
            if (!result)
                return BadRequest(new APiResponds<string>("400", "User or product not found", null));

            return Ok(new APiResponds<string>("200", "Product added to cart", null));
        }



        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new APiResponds<string>("401", "User not authorized", null));

            var items = await _cartService.GetCartItems(int.Parse(userId));
            return Ok(items);
        }



        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

                var deletedItem = await _cartService.RemoveFromCart(productId, userId);

                if (deletedItem != null)
                {
                    return Ok(new APiResponds<CartItems>("200", "Product deleted from cart", deletedItem));
                }

                return BadRequest(new APiResponds<string>("400", "Something went wrong", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APiResponds<string>("500", "Internal Server Error", ex.Message));
            }
        }
        [HttpPost("increment/{cartItemId}")]
        public async Task<IActionResult> IncrementCartItem(int cartItemId, [FromBody] Product product)
        {
            try
            {
                var result = await _cartService.IncrementCartItems(cartItemId, product);
                if (result != null)
                {
                    return Ok(new APiResponds<CartItems>("200", "Quantity incremented", result));
                }
                return BadRequest(new APiResponds<string>("400", "Item not found", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APiResponds<string>("500", "Internal Server Error", ex.Message));
            }
        }
        [HttpPost("decrease/{productId}")]
        public async Task<IActionResult> DecreaseCartItem(int productId)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

                // Now calling DecreaseQuantity with userId and productId
                var result = await _cartService.DecreaseQuantity(userId, productId);

                if (result)
                {
                    return Ok(new APiResponds<string>("200", "Quantity decreased", null));
                }

                return BadRequest(new APiResponds<string>("400", "Item not found or already removed", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APiResponds<string>("500", "Internal Server Error", ex.Message));
            }
        }









    }
}

