
using MyProject.Models.UserModel;
using MyProject.Models.ProductModel;
namespace MyProject.Models.Cart
{
    public class CartItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Users User { get; set; }

    }
}
