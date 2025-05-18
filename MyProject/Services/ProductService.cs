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
            var BYid = await _context.products.FirstOrDefaultAsync(p => p.Id == id);
           
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

        public async Task<string> DeleteProduct(int id)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return "no data";

            }
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return "sucess";
        }

        public async Task<ProductDto?> UpdateProduct(int id, ProductDto updatedProductDto)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }

            // Update the product fields
            product.Name = updatedProductDto.Name;
            product.NewPrice = updatedProductDto.NewPrice;
            product.Category = updatedProductDto.Category;
            product.Description = updatedProductDto.Description;
            product.Image = updatedProductDto.Image;
            product.Rating = updatedProductDto.Rating;
            product.Reviews = updatedProductDto.Reviews;

            // Save changes
            _context.products.Update(product);
            await _context.SaveChangesAsync();

            // Return the updated DTO (including Id if needed)
            return new ProductDto
            {
                 // Include this if ProductDto has Id
                Name = product.Name,
                NewPrice = product.NewPrice,
                Category = product.Category,
                Description = product.Description,
                Image = product.Image,
                Rating = product.Rating,
                Reviews = product.Reviews
            };
        }

    }

}

