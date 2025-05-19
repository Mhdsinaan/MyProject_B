using MyProject.Models.ProductModel;

namespace MyProject.Models.paymentModels
{
    public class PaymentProduct
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public Users? User { get; set; }
        public Product? Product { get; set; }
    }
}
