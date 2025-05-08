using MyProject.Models.ProductModel;

namespace MyProject.Models.CartModel
{
    public class CartDtos
    {
       
            public int ProductId { get; set; }
            public int UserId { get; set; }
            public int Quantity { get; set; }
            public Product Product { get; set; }
            public Users User { get; set; }
        
    }
}
