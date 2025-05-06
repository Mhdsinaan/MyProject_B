using MyProject.Models.User;

namespace MyProject.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUser(RegisterDto request);
        Task<string> LoginUser(LoginDto request);

    }
}
