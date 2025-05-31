using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.ordersModel;
using MyProject.Models.paymentModels;
using MyProject.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(c => c.Product)
                .Where(c => c.UserId == userID)
                .ToListAsync();

            if (!prdcts.Any())
                throw new InvalidOperationException("Cart is empty. Cannot proceed with payment.");

            var totalAmount = prdcts.Sum(c => c.Product.NewPrice * c.Quantity);

            
            var payment = new PaymentProduct
            {
                PaymentDate = DateTime.UtcNow,
                Amount = totalAmount,
                UserId = userID,
                AddressId = request.AddressId,
                Product = prdcts.FirstOrDefault()?.Product
            };

            _context.PaymentProducts.Add(payment);
            // Don't save changes yet because payment.Id is needed for order (depends on identity insert behavior)

            // Prepare list of ProductPurchase entities to save with the order
            List<ProductPurchase> products = new List<ProductPurchase>();
            foreach (var cart in prdcts)
            {
                var productPurchase = new ProductPurchase
                {
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UserId = userID,
                    User = cart.User,
                    Product = cart.Product
                };
                products.Add(productPurchase);
                _context.ProductPurchases.Add(productPurchase);
            }

            // Create order with products
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

            // Remove products from cart after order is placed
            _context.CartProducts.RemoveRange(prdcts);

            // Save all changes in one transaction
            await _context.SaveChangesAsync();

            return payment;
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred while processing the payment", ex);
        }
    }
}
