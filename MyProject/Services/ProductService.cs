using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.Product;

namespace MyProject.Services
{
    public class ProductService : IProductService
    {
        private readonly MyContext _context;
        public ProductService(MyContext context)
        {
            _context = context;
        }
        public async Task<string> GetAllProducts()
        {
            var AllProducts = await _context.products.asyn();
            if (AllProducts == null)
            {
                return null;
            }
            return null;


        }

        

        public async Task<string> GetProductById(int id)
        {
            var BYid=await _context.products.FirstOrDefaultAsync(p => p.Id == id);
            if (BYid == null)
            {
                return null;
            }
            return null;
        }
        
        public Task<string> ProductByCategory(string category)
        {
            var categoryProducts = _context.products.FirstOrDefaultAsync(p => p.Category == category);
            if (categoryProducts == null)
            {
                return null;
            }
            return null;
        }
    }
}
