using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models.ordersModel;
using MyProject.Services;
using MyProject.Interfaces;
using MyProject.CommenApi;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOders _orderServices;

        public OrderController(IOders orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderServices.AllOrder();

            if (orders == null || !orders.Any())
            {
                return NotFound(new APiResponds<string>("404", "No orders found.", null));
            }

            return Ok(new APiResponds<object>("200", "Orders retrieved successfully", orders));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getOrders")]
        public async Task<ActionResult<APiResponds<IEnumerable<Order>>>> GetOrders()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            if (userId == 0)
            {
                return Unauthorized(new APiResponds<string>("401", "Unauthorized access", null));
            }

            var orders = await _orderServices.GetOrders(userId);

            if (orders == null || !orders.Any())
                return NotFound(new APiResponds<string>("404", "No orders found for this user", null));

            return Ok(new APiResponds<IEnumerable<Order>>("200", "Orders retrieved successfully", orders));
        }

        
        [HttpGet("UserDash")]
        public async Task<ActionResult<APiResponds<Dashboard>>> GetUserDash()
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            var dash = await _orderServices.GetUserDashboard(userId);

            if (dash == null)
                return NotFound(new APiResponds<string>("404", "User dashboard not found", null));

            return Ok(new APiResponds<Dashboard>("200", "User dashboard retrieved successfully", dash));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminDash")]
        public async Task<ActionResult<APiResponds<Dashboard>>> GetAdminDash()
        {
            var dash = await _orderServices.GetAdminDashboard();

            if (dash == null)
                return NotFound(new APiResponds<string>("404", "Admin dashboard not found", null));

            return Ok(new APiResponds<Dashboard>("200", "Admin dashboard retrieved successfully", dash));
        }

    }
}
