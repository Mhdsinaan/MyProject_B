using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using MyProject.Models.ProductModel;

namespace MyProject.Services
{
    public class ProductService : IProductService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapping;
        public ProductService(MyContext context,IMapper mapping)
        {
            _context = context;
            _mapping = mapping;
        }
        [HttpGet("GetAll")]
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var allProducts = await _context.products.ToListAsync();
            return _mapping.Map<List<ProductDto>>(allProducts);

        }

       
        public async Task<ProductDto> GetProductById(int id)
        {
            var BYid=await _context.products.FirstOrDefaultAsync(p => p.Id == id);
            if (BYid == null)
            {
                return null;
            }
            return _mapping.Map<ProductDto>(BYid);
        }

        public async Task<List<ProductDto>> ProductByCategory(string category)
        {
            var categoryProducts = await  _context.products.Where(p => p.Category == category).ToListAsync();
            if (categoryProducts == null)
            {
                return null;
            }
            return _mapping.Map<List<ProductDto>>(categoryProducts);
        }
        public async Task<string> AddProduct(ProductDto request)
        {
            var product = _mapping.Map<Product>(request); 

            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();

            return "Product added successfully";
        }

    }
}
