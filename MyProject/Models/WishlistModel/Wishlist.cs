using MyProject.Models.ProductModel;

namespace MyProject.Models.WishlistModel
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Users? User { get; set; }
        public Product? Product { get; set; }
    }
}
