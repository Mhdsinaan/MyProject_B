using System.Collections.Generic;
using MyProject.Models.User;
using MyProject.Models.UserModel;

namespace MyProject.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUser(RegisterDto request);
        Task<LoginResponseDto> LoginUser(LoginDto request);
        Task<string> FindById(int id);
        public Task<string> BlockUser(int id);
        public Task<string> UnblockUser(int id);
        Task<List<Users>> AllUsers();

    }
}
