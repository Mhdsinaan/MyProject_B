using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models.CartModel;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using System.Security.Claims;

namespace MyProject.Services
{
    public class Cartservice : ICartproducts
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyContext _context;
        private readonly IMapper _mapping;

        public Cartservice(MyContext context, IMapper mapping, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapping = mapping;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddToCart(CartDtos cart)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return "Unauthorized";
                }

                int userIdInt = int.Parse(userId);

                var product = await _context.products.FindAsync(cart.ProductId);
                if (product == null) return "Product not found";

                var existingItem = await _context.CartProducts
                    .FirstOrDefaultAsync(c => c.UserId == userIdInt && c.ProductId == cart.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += cart.Quantity;
                    _context.Update(existingItem);
                }
                else
                {
                    var cartItem = new CartItems
                    {
                        UserId = userIdInt,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity
                    };
                    _context.CartProducts.Add(cartItem);
                }

                await _context.SaveChangesAsync();
                return "Item added to cart";
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding to cart", ex);
            }
        }

        public async Task<IEnumerable<CartItems>> GetCartItems(int userId)
        {
            var cartItems = await _context.CartProducts
                .Include(c => c.Product)  
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return cartItems;
        }

        public async Task<CartItems> RemoveFromCart(int id)
        {
            var cartItem = await _context.CartProducts.FindAsync(id);
            if (cartItem == null)
                return null;

            _context.CartProducts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return cartItem;
        }


        public async Task<CartItems> UpdateCart(int id, CartDtos cart, int userId)
        {
            var cartItem = await _context.CartProducts.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (cartItem == null)
                return null;

            cartItem.Quantity = cart.Quantity;
            await _context.SaveChangesAsync();

            return cartItem;
        }

    }
}
