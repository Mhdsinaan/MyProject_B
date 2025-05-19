using Microsoft.EntityFrameworkCore;
using MyProject.Models.Cart;
using MyProject.Models.ordersModel;
using MyProject.Models.paymentModels;
using MyProject.Models.ProductModel;
using MyProject.Models.User;
using MyProject.Models.UserModel;
using MyProject.Models.WishlistModel;
namespace MyProject.Context
{
    public class MyContext:DbContext


    {
        public MyContext(DbContextOptions<MyContext>options):base(options) { }
        public DbSet<Users>users { get; set; }   
        public DbSet<Product> products { get; set; }
        public DbSet<CartItems> CartProducts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<PaymentProduct> PaymentProducts { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
