using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.CommenApi;
using MyProject.Interfaces;
using MyProject.Models.paymentModels;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentService;

        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "User")]
        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentCartDTo request)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
                if (userId == 0)
                {
                    return Unauthorized(new APiResponds<string>("401", "Unauthorized access", null));
                }

                var result = await _paymentService.MakePaymentCart(request, userId);

                if (result == null)
                {
                    return BadRequest(new APiResponds<string>("400", "Payment failed.", null));
                }

                return Ok(new APiResponds<object>("200", "Payment processed successfully.", result));
            }
            catch (Exception ex)
            {
                return BadRequest(new APiResponds<string>("400", $"An error occurred: {ex.Message}", null));
            }
        }
    }
}
