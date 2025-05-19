using MyProject.Models.paymentModels;

namespace MyProject.Interfaces
{
    public interface IPaymentServices
    {
        public Task<PaymentProduct> MakePaymentCart(PaymentCartDTo request, int userID);
    }
}
