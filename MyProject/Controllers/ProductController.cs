using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var allProducts = await _productService.GetAllProducts();
            if (allProducts == null)
                return NotFound();
            return Ok(allProducts);
        }

        [HttpGet]
        public async Task<IActionResult> ByID(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound("No Product found");
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto request)
        {
            var result = await _productService.AddProduct(request);
            if (result == null)
                return BadRequest("Product not added");
            return Ok(result);
        }

        [HttpGet("ProductByCategory")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _productService.ProductByCategory(category);
            if (products == null)
                return NotFound();
            return Ok(products);
        }

       
        [HttpPatch("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto updatedProductDto)
        {
            var result = await _productService.UpdateProduct(id, updatedProductDto);
            if (result == null)
                return NotFound("Product not found or not updated");
            return Ok(result);
        }

   
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result == "no data")
                return NotFound("Product not found");
            return Ok("Deleted Successfully");
        }
    }
}
