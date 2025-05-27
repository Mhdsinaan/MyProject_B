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
    
    public class CartController : ControllerBase
    {
        private readonly ICartproducts _cartService;

        public CartController(ICartproducts cartService)
        {
            _cartService = cartService;
        }


        [HttpPost("add")]
        [Authorize(Roles = "User")]


        public async Task<IActionResult> AddToCart([FromBody] CartDtos cart)
        {
            
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            if (userId == null)

                return Unauthorized(new APiResponds<string>("401", "User not authorized", null));

            var result = await _cartService.AddToCart(cart,userId);
            if (!result)
                return BadRequest(new APiResponds<string>("400", " product not found", null));
            
            return Ok(new APiResponds<string>("200", "Product added to cart","result"));
        }



        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            if (userId == null)
                return Unauthorized(new APiResponds<string>("401", "User not authorized", null));

            var items = await _cartService.GetCartItems(userId);
            return Ok(items);
        }



        [HttpDelete("{productId}")]
        [Authorize(Roles = "User")]
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
        [HttpPost("increment/{productID}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> IncrementCartItem(int productID)
        {
            try
            {
                
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

                
                var result = await _cartService.IncreaseQuantity(userId, productID);

               
                if (result)
                {
                    return Ok(new APiResponds<CartItems>("200", "Quantity incremented", null));
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

