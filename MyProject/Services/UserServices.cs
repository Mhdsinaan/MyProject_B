using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProject.Context;
using MyProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models.User;
using MyProject.Models.User;
using MyProject.Models.UserModel;
namespace MyProject.Services

{
    public class UserServices : IUserService
    {
        private readonly MyContext _context;
        public UserServices(MyContext context)
        {
            _context = context;
        }
        public async Task<string> RegisterUser(RegisterDto request)
        {
            try
            {
                var reg = await _context.users.FirstOrDefaultAsync(p => p.Email == request.Email);
                if(reg != null)
                {
                    return "existing user";
                }
                var user = new Users();
                user.UserName = request.Username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Email = request.Email;
                 _context.users.Add(user);
                await _context.SaveChangesAsync();
                return "User Registered";



            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }

        }
        

        public async Task<string> LoginUser(LoginDto request)
        {
            var logg = await _context.users.FirstOrDefaultAsync(p => p.Email == request.Email);
            if (logg == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, logg.Password)) return null;
            var token = CreateToken(logg);
            return token;
            
        }
        private string CreateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supersecretkeyofbasimhilalisfamousforeverythingthattoughtinthiseraoftechnologyandresearchokthenbye");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    //new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Audience = "MyAppUsers",
                Issuer = "MyApp",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> FindById(int id)
        {

            var Byid = await _context.users.FindAsync(id);
            if (Byid == null)
            {
                return null;
            }
            return Byid.UserName; 
           
        }
    }
}