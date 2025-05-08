using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.User
{
    public class RegisterDto
    {
        
        
        public required string Username { get; set; }
        [Required]

        public required string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
