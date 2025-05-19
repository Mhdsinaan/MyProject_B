using MyProject.Models.paymentModels;

namespace MyProject.Interfaces
{
    public interface IPaymentServices
    {
        public Task<PaymentCart> MakePaymentCart(PaymentCartDTo request, int userID);
    }
}
