using Microsoft.EntityFrameworkCore;
using MyProject.Models.Product;
using MyProject.Models.User;
namespace MyProject.Context
{
    public class MyContext:DbContext


    {
        public MyContext(DbContextOptions<MyContext>options):base(options) { }
        public DbSet<User>users { get; set; }   
        public DbSet<Product> products { get; set; }

    }
}
