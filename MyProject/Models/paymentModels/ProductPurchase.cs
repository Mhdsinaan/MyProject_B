using MyProject.Models.ProductModel;

namespace MyProject.Models.paymentModels
{
    public class ProductPurchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
