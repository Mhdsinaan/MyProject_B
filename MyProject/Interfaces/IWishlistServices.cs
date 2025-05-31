using System.Threading.Tasks;
using MyProject.Models.WishlistModel;

namespace MyProject.Interfaces
{
    public interface IWishlistServices
    {
        public Task<IEnumerable<WishListResDTO>> GetWishlist(int userId);
        public Task<IEnumerable<WishlistDto>> AddToWishlist(WishlistDto wishlist, int userId);
        public Task<string> RemoveWishlist(int id, int userId);
    }
}
