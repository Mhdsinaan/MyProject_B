using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetALL")]
        public IActionResult Index()
        {
            var AllProducts = _productService.GetAllProducts();
            if (AllProducts == null)
            {
                return NotFound();
            }
            return Ok(AllProducts);

        }
    }
}
