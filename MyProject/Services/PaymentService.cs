using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.ordersModel;
using MyProject.Models.paymentModels;
using MyProject.Models.ProductModel;

public class PaymentService : IPaymentServices
{
    private readonly MyContext _context;

    public PaymentService(MyContext context)
    {
        _context = context;
    }

    public async Task<PaymentProduct> MakePaymentCart(PaymentCartDTo request, int userID)
    {



        try
        {

            var prdcts = await _context.CartProducts
                .Include(c => c.product)
                .Where(c => c.UserId == userID)
                .ToListAsync();

            if (!prdcts.Any())
                throw new InvalidOperationException("Cart is empty. Cannot proceed with payment.");


            var totalAmount = prdcts.Sum(c => c.product.NewPrice * c.Quantity);


            var payment = new PaymentProduct
            {
                PaymentDate = DateTime.UtcNow,
                Amount = totalAmount,
                UserId = userID,
                AddressId = request.AddressId,
                //Product = product
            };

            _context.PaymentProducts.Add(payment);
            await _context.SaveChangesAsync();


            List<ProductPurchase> products = prdcts.Select(cart => new ProductPurchase
            {
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                UserId = cart.UserId

            }).ToList();

            _context.ProductPurchases.AddRange(products);


            var order = new Order
            {
                Amount = totalAmount,
                UserId = userID,
                OrderDate = DateTime.UtcNow,
                PaymentMode = "Cart",
                PaymentId = payment.Id,
                Quantity = prdcts.Sum(c => c.Quantity),
                Products = products,
                AddressId = request.AddressId
            };

            _context.Orders.Add(order);


            _context.CartProducts.RemoveRange(prdcts);


            await _context.SaveChangesAsync();

            return payment;
        }
        catch (Exception ex)
        {

            throw new Exception("Error occurred while processing the payment", ex);
        }
    }
}
