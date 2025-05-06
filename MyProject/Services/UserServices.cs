using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.User;
using System.Security.Claims;
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
                if (await _context.users.AnyAsync(p => p.Email == request.Email))
                {
                    return null;

                }
                var user = new User();
                user.UserName = request.UserName;
                user.Email = request.Email;
                user.Password = request.Password;
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
                return "user registration successfull";




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
        private string CreateToken(User user)
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}