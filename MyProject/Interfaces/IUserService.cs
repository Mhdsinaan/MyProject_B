using MyProject.Models.User;
using MyProject.Models.UserModel;

namespace MyProject.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUser(RegisterDto request);
        Task<string> LoginUser(LoginDto request);
        Task<string> FindById(int id);
       
    }
}
