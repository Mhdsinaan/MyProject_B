
using MyProject.Models.ProductModel;

namespace MyProject.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task<List<ProductDto>> ProductByCategory(string category);


        Task<string> AddProduct(ProductDto request);
    }
}
