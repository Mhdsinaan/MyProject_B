

using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using MyProject.Models.ProductModel;

namespace MyProject.Interfaces
{
    public interface ICartproducts
    {
        public Task<IEnumerable<CartDtos>> GetCartItems(Users userId);
        public Task<string> AddToCart(CartDtos cart);
        public Task<CartItems> RemoveFromCart(int id, Users userid);
        public Task<CartItems> incrementCartItems(int id, Product produ);
        
    }
}

