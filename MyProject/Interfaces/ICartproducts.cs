

using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using MyProject.Models.ProductModel;

namespace MyProject.Interfaces
{
    public interface ICartproducts
    {
        public Task<IEnumerable<CartDtos>> GetCartItems(int userId);
        public Task<bool> AddToCart(CartDtos cart, int userId);
        public Task<CartItems?> RemoveFromCart(int productId, int userId);
        public Task<CartItems> IncrementCartItems(int id, Product product);
        public Task<bool> DecreaseQuantity(int userId, int productId);


    }
}

