using MyProject.Models.Cart;

namespace MyProject.Models.paymentModels
{
    public class PaymentCart
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public Users? User { get; set; }
        public ICollection<CartItems>? Product { get; set; }
    }
}
