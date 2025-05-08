using Microsoft.EntityFrameworkCore;
using MyProject.Models.Cart;

using MyProject.Models.ProductModel;
using MyProject.Models.User;
using MyProject.Models.UserModel;
namespace MyProject.Context
{
    public class MyContext:DbContext


    {
        public MyContext(DbContextOptions<MyContext>options):base(options) { }
        public DbSet<Users>users { get; set; }   
        public DbSet<Product> products { get; set; }
        public DbSet<CartItems> CartProducts { get; set; }

    }
}
