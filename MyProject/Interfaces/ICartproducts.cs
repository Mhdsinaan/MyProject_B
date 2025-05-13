

using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using MyProject.Models.ProductModel;

namespace MyProject.Interfaces
{
    public interface ICartproducts
    {
        public Task<IEnumerable<CartDtos>> GetCartItems(int userId);
        public Task<bool> AddToCart(CartDtos cart, int usreid);
        public Task<CartItems> RemoveFromCart(int id, Users userid);
        public Task<CartItems> incrementCartItems(int id, Product produ);
        
    }
}

