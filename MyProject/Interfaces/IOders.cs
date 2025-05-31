using MyProject.Models.ordersModel;

namespace MyProject.Interfaces
{
    public interface IOders
    {
        public Task<IEnumerable<Order>> GetOrders(int userId);
        public Task<Dashboard> GetAdminDashboard();
        public Task<Dashboard> GetUserDashboard(int userId);
        public Task<IEnumerable<OrderDto>> AllOrder();

    }
}
