using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Interfaces;
using MyProject.Models.ProductModel;


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
        public async Task<IActionResult> GetAll()
        {
            var AllProducts = await _productService.GetAllProducts();
            if (AllProducts == null)
            {
                return NotFound();
            }
            return Ok(AllProducts);

        }
        [HttpGet]
        public async Task<IActionResult>ByID(int id)
        {
            var user = await _productService.GetProductById(id);
            if (user == null)
            {
                return NotFound("No User found");

            }
            return Ok(user);
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto request)
        {
            var result = await _productService.AddProduct(request);
            if (result == null)
            {
                return BadRequest("Product not added");
            }
            return Ok(result);
        }
        [HttpGet("ProductByCategory")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var Products = await _productService.ProductByCategory(category);
            if (Products == null)
            {
                return NotFound();
            }
            return Ok(Products);
        }
    }
}
