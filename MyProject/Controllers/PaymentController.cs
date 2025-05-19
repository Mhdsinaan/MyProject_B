using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;
using MyProject.Models.paymentModels;

namespace MyProject.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentServices _paymentService;
        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentCartDTo request)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
                if (userId == null) return Unauthorized();

                var result = await _paymentService.MakePaymentCart(request, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
