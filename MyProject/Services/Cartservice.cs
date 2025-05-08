using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;

namespace MyProject.Services
{
    public class CartService : ICartproducts
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        public CartService(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> AddToCart(CartDtos cart)
        {
            try
            {
                var cartProduct = _mapper.Map<CartItems>(cart);
                await _context.CartProducts.AddAsync(cartProduct);
                return "Item added to the cart successfully!";
            } catch (Exception ex)
            {
                return $"Error adding item to cart: {ex.Message}";

            }
        }

        public async Task<IEnumerable<CartItems>> GetCartItems(int id)
        {
            var cartItems = await _context.CartProducts
                .Where(c => c.UserId == id).ToListAsync();
            return _mapper.Map<IEnumerable<CartItems>>(cartItems);


        }

        public async Task<CartItems> RemoveFromCart(int id)
        {
            var remove = await _context.CartProducts.FindAsync(id);
            if (remove == null)
            {
                throw new Exception("Item not found in the cart");
            }
            _context.CartProducts.Remove(remove);
            await _context.SaveChangesAsync();
            return _mapper.Map<CartItems>(remove);
        }

        public async Task<CartItems> Increment(int id, CartDtos cart, int userId)
        {

            var cartProduct = await _context.CartProducts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (cartProduct == null)
            {
                throw new Exception("Item not found in the cart");
            }
            cartProduct.Quantity += 1;

            await _context.SaveChangesAsync();
            return _mapper.Map<CartItems>(cartProduct);

        }
        public async Task<CartItems> Decrement(int id, int userId)
        {
           
            var cartProduct = await _context.CartProducts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (cartProduct == null)
            {
                throw new Exception("Item not found in the cart");
            }

           
            if (cartProduct.Quantity > 1)
            {
                cartProduct.Quantity -= 1;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Quantity cannot be less than 1. You can remove the item instead.");
            }

            
            return _mapper.Map<CartItems>(cartProduct);
        }

    }
}
