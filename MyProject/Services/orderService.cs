using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.ordersModel;

namespace MyProject.Services
{
    public class orderService : IOders
    {
        private readonly MyContext _context;
        public orderService(MyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> AllOrder()
        {
            var allOrders = await _context.Orders
                .Include(o => o.Products)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Products)
                    .ThenInclude(p => p.User)
                .ToListAsync();

            var result = allOrders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
               
                Amount = order.Amount,
                Quantity = order.Quantity,
                UserName = order.Products.FirstOrDefault()?.User?.UserName ?? "N/A",
                ProductName = order.Products.FirstOrDefault()?.Product?.Name ?? "N/A",
                ProductImage = order.Products.FirstOrDefault()?.Product?.Image ?? ""
            });

            return result;
        }


        public async Task<Dashboard> GetAdminDashboard()
        {
            try
            {
                var order =await _context.Orders.ToListAsync();
                if(order == null)
                {
                    throw new InvalidOperationException("No orders found.");
                }
                var dashbourd = new Dashboard();
                dashbourd.totalOrders = order.Count;
                dashbourd.totalRevenue = order.Sum(o => o.Amount);
                dashbourd.totalProducts = order.Sum(o => o.Quantity);
                return dashbourd;



            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }


        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            try
            {
                var orders =  await _context.Orders.Include(p => p.Products).Where(o => o.UserId == userId).ToListAsync();
                if (orders is null) return null;
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching orders", ex);
            }
        }

        public async Task<Dashboard> GetUserDashboard(int userId)
        {
            var orders= await _context.Orders.Include(p => p.Products).Where(o => o.UserId == userId).ToListAsync();
            if (orders == null )
            {
                throw new InvalidOperationException("No orders found for the user.");
            }
            var dashboard = new Dashboard
            {
                totalOrders = orders.Count,
                totalRevenue = orders.Sum(o => o.Amount),
                //totalProducts = orders.Sum(o => o.product.Count)
            };
            return dashboard;   

        }
    }
}
