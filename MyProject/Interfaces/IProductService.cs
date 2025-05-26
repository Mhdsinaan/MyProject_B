
using MyProject.Models.ProductModel;

namespace MyProject.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task<List<ProductDto>> ProductByCategory(string category);


        Task<string> AddProduct(ProductDto request);
        Task<string> DeleteProduct(int id);
        Task<ProductDto> UpdateProduct(int id, ProductDto updatedProductDto);
    }
}
