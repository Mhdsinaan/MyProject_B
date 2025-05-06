namespace MyProject.Interfaces
{
    public interface IProductService
    {
        Task <string>GetAllProducts();
        Task<string> GetProductById(int id);
        Task<string> ProductByCategory(string category);
    }
}
