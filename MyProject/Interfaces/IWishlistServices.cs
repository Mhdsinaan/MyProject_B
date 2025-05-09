using MyProject.Models.WishlistModel;

namespace MyProject.Interfaces
{
    public interface IWishlistServices
    {
        public Task<IEnumerable<Wishlist>> GetWishlist(int userId);
        public Task<string> AddToWishlist(WishlistDto wishlist, int userId);
        public Task<string> RemoveWishlist(int id, int userId);
    }
}
