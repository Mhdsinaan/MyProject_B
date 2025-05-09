

using MyProject.Models.Cart;
using MyProject.Models.CartModel;

namespace MyProject.Interfaces
{
    public interface ICartproducts
    {
        public Task<IEnumerable<CartItems>> GetCartItems(Users userId);
        public Task<string> AddToCart(CartDtos cart);
        public Task<CartItems> RemoveFromCart(int id, Users userid);
        public Task<CartItems> UpdateCart(int id, CartDtos cart, Users userId);
    }
}

