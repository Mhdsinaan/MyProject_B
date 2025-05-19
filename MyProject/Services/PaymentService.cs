using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.ordersModel;
using MyProject.Models.paymentModels;

public class PaymentService : IPaymentServices
{
    private readonly MyContext _context;

    public PaymentService(MyContext context)
    {
        _context = context;
    }

    public async Task<PaymentCart> MakePaymentCart(PaymentCartDTo request, int userID)
    {
        try
        {
            var cartItems = await _context.CartProducts
                .Include(c => c.Product)
                .Where(c => c.UserId == userID)
                .ToListAsync();

            if (!cartItems.Any()) return null;

            var totalAmount = cartItems.Sum(c => c.Product.NewPrice * c.Quantity);

            var payment = new PaymentCart
            {
                Amount = totalAmount,
                UserId = userID,
                PaymentDate = DateTime.UtcNow,
                Product = cartItems,
                AddressId = request.AddressId
            };

            _context.PaymentProducts.Add(payment);
            await _context.SaveChangesAsync(); // Save to get Payment ID

            List<ProductPurchase> products = new();
            foreach (var cart in cartItems)
            {
                var productPurchase = new ProductPurchase
                {
                    ProductId = cart.ProductId,
                    Product = cart.Product,
                    Quantity = cart.Quantity,
                    UserId = cart.UserId,
                    User = cart.User
                };
                products.Add(productPurchase);
                _context.ProductPurchases.Add(productPurchase);
            }

            var order = new Order
            {
                Amount = totalAmount,
                UserId = userID,
                OrderDate = DateTime.UtcNow,
                PaymentMode = "Cart",
                PaymentId = payment.Id,
                Quantity = cartItems.Sum(c => c.Quantity),
                Products = products,
                AddressId = request.AddressId
            };

            _context.Orders.Add(order);
            _context.CartProducts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
            return payment;
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred while processing the payment", ex);
        }
    }
}
