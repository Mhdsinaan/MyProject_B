

using MyProject.Models.Cart;
using MyProject.Models.CartModel;

namespace MyProject.Interfaces
{
    public interface ICartproducts
    {
        public Task<IEnumerable<CartItems>> GetCartItems( int id);
        public Task<string> AddToCart(CartDtos cart);
        public Task<CartItems> RemoveFromCart(int id );
        public Task<CartItems> UpdateCart(int id, CartDtos cart, int userId);
    }
}

