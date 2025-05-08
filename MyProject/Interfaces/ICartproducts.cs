

using MyProject.Models.Cart;
using MyProject.Models.CartModel;

namespace MyProject.Interfaces
{
    public interface ICartproducts
    {
        public Task<IEnumerable<CartItems>> GetCartItems( int id);
        public Task<string> AddToCart(CartDtos cart);
        public Task<CartItems> RemoveFromCart(int id );
    
        Task<CartItems> Increment(int id, CartDtos cart, int userId);
        Task<CartItems> Decrement(int id, int userId);
    }
}

