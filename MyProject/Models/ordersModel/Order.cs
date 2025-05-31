using MyProject.Models.paymentModels;
using MyProject.Models.ProductModel;

namespace MyProject.Models.ordersModel
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PaymentMode { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public List<ProductPurchase>? Products { get; set; }
       
        public Product? product { get; set; }
        public Users? User { get; set; }
    }
}
